using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Threading;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WebSocketSharp;

using MisaMinoNET;
using PerfectClearNET;

using Zetris.BotrisBattle.Messages;

namespace Zetris.BotrisBattle {
    class BotrisBattleBot: Bot<UI, BotrisBattleBot> {
        protected override int getPreviews() => int.MaxValue;
        protected override bool getPerfectClear() => Preferences.PerfectClear;
        protected override bool getEnhancePerfect() => Preferences.PerfectClear;
        protected override bool HoldAllowed() => true;
        protected override AllowedSpins getAllowedSpins() => AllowedSpins.AllSpins;
        protected override MisaMinoParameters CurrentStyle() => Preferences.CurrentStyle.Clone().Parameters;
        protected override bool C4W() => false;
        protected override bool TSDOnly() => false;
        protected override int Intelligence() => Preferences.Intelligence;
        protected override bool Allow180() => false;
        protected override TetrisGame getTetrisGame() => TetrisGame.BotrisBattle;
        protected override bool getAllowTmini() => true;
        protected override uint PCThreads() => Preferences.PCThreads;
        protected override bool GarbageBlocking() => true;
        protected override int RushTime() => 80;
        protected override bool Danger() => false; // there is no PUBLIC version of BotrisBattle bot

        const int SpawnPos = 21;

        static int? ConvertBlock(string block) {
            if (block == null) return null;
            if (block == "G") return 9;

            int i = Array.IndexOf(MisaMino.ToChar, block);

            if (i == -1) throw new Exception($"Invalid block {block}");
            return i;
        }

        static int ConvertBlock255(string block)
            => ConvertBlock(block)?? 255;

        void ReadBoard(string[][] gameboard, int[,] to) {
            for (int j = 0; j < 40; j++)
                for (int i = 0; i < 10; i++)
                    to[i, j] = j < gameboard.Length
                        ? ConvertBlock255(gameboard[j][i])
                        : 255;
        }

        int[,] ReadBoard(string[][] gameboard) {
            int[,] readboard = new int[10, 40];
            ReadBoard(gameboard, readboard);
            return readboard;
        }

        void LoadBoard(string[][] gameboard) {
            ReadBoard(gameboard, board);
        }

        Stopwatch timer;
        long startedThinkingAt = 0;
        Types.RoomSettings settings;

        void StartThinking(int garbage) {
            int[] q = getClippedQueue();

            MisaMinoAOT(current, q, hold, combo, garbage, SpawnPos);

            if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

            PerfectClearAOT(current, q, hold, combo);

            startedThinkingAt = timer.ElapsedMilliseconds;
        }

        static readonly Dictionary<Instruction, string> ToBotris = new Dictionary<Instruction, string>() {
            {Instruction.HOLD, "hold"},
            {Instruction.L, "move_left"},
            {Instruction.R, "move_right"},
            {Instruction.LL, "sonic_left"},
            {Instruction.RR, "sonic_right"},
            {Instruction.RSPIN, "rotate_cw"},
            {Instruction.LSPIN, "rotate_ccw"},
            {Instruction.D, "drop"},
            {Instruction.DD, "sonic_drop"},
        };

        static string[] InstructionsToCommands(List<Instruction> ins)
            => ins.Where(i => ToBotris.ContainsKey(i)).Select(i => ToBotris[i]).ToArray();

        void RoundOver() {
            EndGame();
            Window.Active = false;
        }

        string self = null;

        public string Token = null;
        public string RoomKey = null;

        WebSocket ws = null;
        bool wasClosed = false;

        Dictionary<string, Func<JToken, Message>> handlers;

        BlockingCollection<Message> msgq = new BlockingCollection<Message>();

        protected override void LoopIteration() {
            Message msg;

            try {
                msg = msgq.Take();

            } catch (InvalidOperationException) {
                // queue is complete
                return;
            }

            var response = handlers[msg.type](msg.payload);

            if (response != null)
                ws.Send(JsonConvert.SerializeObject(response));
        }

        protected override void Starting() {
            if (string.IsNullOrWhiteSpace(Token))
                throw new Exception("Token is not set");

            if (string.IsNullOrWhiteSpace(RoomKey))
                throw new Exception("RoomKey is not set");

            ws = new WebSocket($"wss://botrisbattle.com/ws?token={Token}&roomKey={RoomKey}");
            ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;

            ws.OnMessage += (s, e) => {
                msgq.Add(JsonConvert.DeserializeObject<Message>(e.Data));
            };

            ws.OnClose += (s, e) => {
                if (!wasClosed)
                    throw new Exception("WebSocket closed unexpectedly");
            };

            ws.OnError += (s, e) => {
                // fuck websocketsharp
                SynchronizationContext.Current.Post(_ => throw e.Exception, null);
            };

            handlers = new Dictionary<string, Func<JToken, Message>> {
                {"room_data", payload => {
                    var roomData = payload.ToObject<RoomDataPayload>().roomData;
                    return null;
                }},

                {"authenticated", payload => {
                    self = payload.ToObject<AuthenticatedPayload>().sessionId;
                    return null;
                }},

                {"error", payload => {
                    var error = payload.ToObject<string>();
                    return null;
                }},

                {"player_joined", payload => {
                    var playerData = payload.ToObject<PlayerJoinedPayload>().playerData;
                    return null;
                }},

                {"player_left", payload => {
                    var sessionId = payload.ToObject<PlayerLeftPayload>().sessionId;
                    return null;
                }},

                {"player_banned", payload => {
                    var botInfo = payload.ToObject<PlayerBannedPayload>().botInfo;
                    return null;
                }},

                {"player_unbanned", payload => {
                    var botInfo = payload.ToObject<PlayerUnbannedPayload>().botInfo;
                    return null;
                }},

                {"settings_changed", payload => {
                    var roomData = payload.ToObject<SettingsChangedPayload>().roomData;
                    return null;
                }},

                {"game_started", _ => {
                    return null;
                }},

                {"round_started", payload => {
                    var data = payload.ToObject<RoundStartedPayload>();
                    var gameState = data.roomData.players.Single(i => i.sessionId == self).gameState;
                    settings = data.roomData.settings;

                    RoundOver();

                    timer?.Stop();
                    timer = Stopwatch.StartNew();
                    startedThinkingAt = 0;

                    NewGame(() => {
                        LoadBoard(gameState.board);

                        current = ConvertBlock255(gameState.current.piece);
                        queue = gameState.queue.Select(ConvertBlock255).ToList();
                        hold = ConvertBlock(gameState.held);
                        combo = gameState.combo;
                    }, baseBoardHeight = SpawnPos);

                    Window.Active = true;

                    return null;
                }},

                {"request_move", payload => {
                    if (!Window.Active) return null;

                    var data = payload.ToObject<RequestMovePayload>();

                    garbage = data.gameState.garbageQueued?.Length?? 0;

                    misaboard = (int[,])board.Clone();
                    pcboard = (int[,])board.Clone();

                    if (!MisaMino.Running && MisaMino.LastSolution == null)
                        StartThinking(garbage);

                    double minWait = startedThinkingAt + (settings.pps >= 10? 0: Math.Max(RushTime(), 1000.0 / settings.pps / 2));

                    while (timer.ElapsedMilliseconds < minWait);

                    bool applied = MakeDecision(out bool wasHold, out int clear, out _);
                    MisaMino.LastSolution = null;

                    if (wasHold) {  // Advance game state since we manage it internally, even if the piece didn't end up placing...
                        if (hold == null) queue.RemoveAt(0);
                        hold = current;
                    }

                    current = queue[0];
                    queue.RemoveAt(0);

                    if (clear > 0) combo++;
                    else combo = 0;
                     
                    if (applied)
                        StartThinking(clear > 0? garbage - atk : Math.Max(0, garbage - 8));

                    return Message.Create("action", new ActionPayload() {
                        commands = InstructionsToCommands(movements)
                    });
                }},

                {"player_action", payload => {
                    var data = payload.ToObject<PlayerActionPayload>();

                    if (data.sessionId == self) {
                        foreach (var i in data.events.Where(i => i.type == "queue_added")) {
                            queue.Add(ConvertBlock255(i.payload.ToObject<Types.QueueAddedGameEventPayload>().piece));
                        }

                        if (data.events.Any(i => i.type == "damage_tanked") ||
                            !BoardEquals(board, ReadBoard(data.gameState.board))
                        ) {
                            LoadBoard(data.gameState.board);

                            misaboard = (int[,])board.Clone();
                            pcboard = (int[,])board.Clone();

                            StartThinking(data.gameState.garbageQueued?.Length?? 0);
                        }
                    }

                    return null;
                }},

                {"player_damage_received", payload => {
                    var data = payload.ToObject<PlayerDamageReceivedPayload>();
                    return null;
                }},

                {"round_over", payload => {
                    var data = payload.ToObject<RoundOverPayload>();

                    RoundOver();
                    return null;
                }},

                {"game_over", payload => {
                    var data = payload.ToObject<GameOverPayload>();
                    return null;
                }},

                {"game_reset", payload => {
                    var roomData = payload.ToObject<RoomDataPayload>().roomData;
                    return null;
                }},

                {"ping", payload => {
                    var timestamp = payload.ToObject<PingPayload>().timestamp;
                    return null;
                }}
            };

            ws.Connect();
        }

        protected override void BeforeDispose() {
            wasClosed = true;
            ws?.Close();
            msgq.CompleteAdding();
        }
    }
}
