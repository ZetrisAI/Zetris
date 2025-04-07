using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MisaMinoNET;
using PerfectClearNET;

namespace Zetris.TETRIO {
    class TetrioBot: Bot<UI, TetrioBot> {
        #region InputHelper
        static void AddGarbage(int[,] board, int[] garbage, int attack, out int[] leftover) {
            leftover = garbage.Take(garbage.Length - attack).ToArray();

            if (leftover.Length <= 0) return;

            int m = Math.Min(8, leftover.Length);

            for (int i = 0; i < m; i++)
                AddGarbageLine(board, leftover[i]);

            leftover = garbage.Skip(m).ToArray();
        }
        #endregion

        protected override int getPreviews() => (Preferences.Previews > 18)? int.MaxValue : Preferences.Previews;
        protected override bool getPerfectClear() => Preferences.PerfectClear;
        protected override bool getEnhancePerfect() => Preferences.EnhancePerfect;
        protected override bool HoldAllowed() => Preferences.HoldAllowed;
        protected override AllowedSpins getAllowedSpins() => Preferences.AllSpins? AllowedSpins.AllSpins : AllowedSpins.TSpins;
        protected override MisaMinoParameters CurrentStyle() => Preferences.CurrentStyle.Clone().Parameters;
        protected override bool C4W() => Preferences.C4W;
        protected override bool TSDOnly() => Preferences.TSDOnly;
        protected override int Intelligence() => Preferences.Intelligence;
        protected override bool Allow180() => true;
        protected override TetrisGame getTetrisGame() => Preferences.AllSpins? TetrisGame.TETRIOS2 : TetrisGame.TETRIO;
        protected override bool getAllowTmini() => true;
        protected override uint PCThreads() => Preferences.PCThreads;
        protected override bool GarbageBlocking() => true;
        protected override int RushTime() => 40;
        protected override bool Danger() => false; // there is no PUBLIC version due to TETR.IO not having local multiplayer

        static int? ConvPiece(string p) {
            if (p == null) return null;
            if (p == "GB") return 9;
            
            int i = Array.IndexOf(MisaMino.ToChar, p);

            if (i == -1) throw new Exception($"Invalid piece {p}");
            return i;
        }

        static int ConvPiece255(string p) => ConvPiece(p)?? 255;

        static readonly Dictionary<Instruction, string> ToTetrio = new Dictionary<Instruction, string>() {
            {Instruction.L, "Left"},
            {Instruction.R, "Right"},
            {Instruction.LL, "DASLeft"},
            {Instruction.RR, "DASRight"},
            {Instruction.D, "SonicDrop"},
            {Instruction.DD, "SonicDrop"},
            {Instruction.LSPIN, "Ccw"},
            {Instruction.RSPIN, "Cw"},
            {Instruction.SPIN2, "180"},
            {Instruction.HOLD, "Hold"},
            {Instruction.DROP, "HardDrop"}
        };

        int pieceCount = -1;
        Stopwatch timer;

        long startedThinkingAt = 0;
        
        void startThinking(int garbage) {
            int[] q = getClippedQueue();

            LogHelper.LogText("QUEUE FOR AOT: " + string.Join(" ", q));

            MisaMinoAOT(current, q, hold, combo, garbage, 22 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 3, 0)));

            if (PerfectClear.Running && !pcbuffer) PerfectClear.Abort();

            PerfectClearAOT(current, q, hold, combo);

            startedThinkingAt = timer.ElapsedMilliseconds;
        }

        void loadBoardFromJSON(JToken json) {
            string[,] reset = json.ToObject<string[,]>();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 40; j++)
                    board[i, j] = ConvPiece255(reset[39 - j, i]?.ToUpper());
        }

        void receivedGame(JToken e) {
            game_session_id = e["id"].ToObject<int?>();

            NewGame(() => {
                loadBoardFromJSON(e["board"]);

                IEnumerable<int> incoming = e["pieces"].ToObject<string[]>().Select(ConvPiece255);
                current = incoming.First();
                queue = incoming.Skip(1).ToList();
                hold = ConvPiece(e["hold"].ToObject<string>());
                combo = 0;
            }, 22);

            baseBoardHeight = 22;
        }

        static string[] InstructionsToMovesArray(List<Instruction> ins)
            => ins.Where(i => ToTetrio.ContainsKey(i)).Select(i => ToTetrio[i]).ToArray();

        public bool IsZETRIO = false;

        public void ToZETRIO(string command, object data) {
            Console.WriteLine($"zetrio:{JToken.FromObject(new { command, data }).ToString(Formatting.None)}");
        }

        int? game_session_id = null;

        const ushort DefaultPort = 9387;
        public ushort Port = DefaultPort;

        public bool UseLegacyHTTP = false;
        TetrioClientConnection server;

        protected override void LoopIteration()
            => server.LoopIteration();

        protected override void Starting() {
            for (int i = 0; i < 32; i++) {
                var result = UseLegacyHTTP
                    ? TetrioClientConnectionHTTP.Start(Port, out server)
                    : TetrioClientConnectionWS.Start(Port, out server);

                if (!result) {
                    Port++;
                    continue;
                }

                break;
            }

            if (Port != DefaultPort)
                Window?.SetPortTitle(UseLegacyHTTP, Port);

            if (IsZETRIO)
                ToZETRIO("port", Port);

            server.RegisterHandler("newGame", e => {
                server.InvokeHandler("endGame", null);

                receivedGame(e);
                pieceCount = 0;

                Window.Active = true;
                return null;
            });

            server.RegisterHandler("resetGame", e => {
                if (pieceCount == -1)
                    return null;

                receivedGame(e);
                return null;
            });

            server.RegisterHandler("newPieces", e => {
                if (pieceCount != -1)
                    queue.AddRange(e.ToObject<string[]>().Select(ConvPiece255));

                return null;
            });

            server.RegisterHandler("nextMove", e => {
                if (pieceCount == -1)
                    return new {
                        moves = new string[0],
                        expected_cells = new int[0][]
                    };

                garbage = (int)e;

                misaboard = (int[,])board.Clone();
                pcboard = (int[,])board.Clone();

                if (pieceCount == 0) {
                    timer?.Stop();
                    timer = Stopwatch.StartNew();
                    startedThinkingAt = 0;

                } else {
                    if (!MisaMino.Running && MisaMino.LastSolution == null)
                        startThinking(garbage);
                    
                    double minWait = startedThinkingAt + (Preferences.Speed >= 10? 0 : Math.Max(RushTime(), 1000.0 / Preferences.Speed / 2));
                    double nextPiece = Preferences.Speed >= 30 ? 0 : (pieceCount * 1000.0 / Preferences.Speed);
                    double waitUntil = Math.Max(nextPiece, minWait);

                    while (timer.ElapsedMilliseconds < (pieceCount <= 0? 0 : waitUntil));
                }

                pieceCount++;

                // ideally this should take gravity into account...  i'm too lazy
                baseBoardHeight = 22 + Convert.ToInt32(!FitPieceWithConvert(misaboard, current, 4, 3, 0));

                bool applied = MakeDecision(out bool wasHold, out int clear, out List<int[]> coords);
                MisaMino.LastSolution = null;

                LogHelper.LogText($"Placed {pieceUsed}");

                if (wasHold) {  // In TETR.IO, advance game state since we manage it internally, even if the piece didn't end up placing...
                    if (hold == null) queue.RemoveAt(0);
                    hold = current;
                }

                current = queue[0];
                queue.RemoveAt(0);

                if (clear > 0) combo++;
                else combo = 0;
                     
                if (applied) {
                    LogHelper.LogText("Thinking...");
                    startThinking(clear > 0? garbage - atk : Math.Max(0, garbage - 8));
                }

                return new {
                    id = game_session_id,
                    moves = InstructionsToMovesArray(movements),
                    expected_cells = coords.ToArray()
                };
            });

            server.RegisterHandler("recovery", e => {
                if (pieceCount == -1)
                    return new {
                        moves = new string[0]
                    };

                var recovery = Recovery(
                    (int)e["x"],
                    (int)Math.Floor((double)e["y"]) - 14,
                    (int)e["r"],
                    out bool recovered
                );

                return new {
                    id = game_session_id,
                    moves = InstructionsToMovesArray(recovery)
                };
            });

            server.RegisterHandler("resetBoard", _e => {
                if (pieceCount == -1) return null;

                dynamic e = (dynamic)_e;
                loadBoardFromJSON(e.board);

                misaboard = (int[,])board.Clone();
                pcboard = (int[,])board.Clone();

                LogHelper.LogText("ResetBoard thinking...");
                startThinking((int)e.garbage);

                return null;
            });

            server.RegisterHandler("endGame", e => {
                EndGame();

                pieceCount = -1;

                Window.Active = false;
                return null;
            });

            server.RegisterHandler("speedDown", e => {
                Window?.SetSpeed(Preferences.Speed * 10 / 11);
                return null;
            });

            server.RegisterHandler("speedUp", e => {
                Window?.SetSpeed(Preferences.Speed / 10 * 11);
                return null;
            });

            Dictionary<string, ChatCommandBase> chatCommands = null;

            server.RegisterHandler("chatCommand", e => {
                IEnumerable<string> split = ((string)e).Trim().Split(' ').Select(i => i.Trim());

                string command = split.First();
                string[] args = split.Skip(1).ToArray();

                if (command.Length == 0 || command.Any(i => !"0123456789abcdefghijklmnopqrstuvwxyz".Contains(i)))
                    return null;

                if (!Preferences.ChatCommands)
                    return "Chat commands are disabled at the moment.";

                foreach (var i in chatCommands)
                    if (i.Key == command) {
                        if (Window.Active && !i.Value.EvenWhileActive)
                            return "Can't do that while in-game.";

                        return i.Value.Process(args);
                    }

                return Window.Active? null : "Unknown command.";
            });

            chatCommands = new Dictionary<string, ChatCommandBase>() {
                {"help", new ChatCommand(
                    "Displays all available commands.",
                    () => "Available commands:\n" +
                        string.Join("\n",
                            chatCommands
                                .Where(i => !Window.Active || i.Value.EvenWhileActive)
                                .Select(i => $".{i.Key} {string.Join("", i.Value.Hints.Select(j => $"<{j}> "))}- {i.Value.HelpText}")
                        ),
                    true
                )},

                {"pps", new ChatCommand<double>(
                    "Sets the speed (PPS).",
                    "speed",
                    s => s.ToLower() == "inf"? (double?)30 : null,
                    e => {
                        if (e < 0.1)
                            return ChatCommandBase.InvalidParameters;

                        Window?.SetSpeed(Math.Round(e, 2)).Wait();
                        return null;
                    },
                    () => $"pps = {(Preferences.Speed >= 30? "inf" : Preferences.Speed.ToString())}"
                )},

                {"pcf", new OnOffChatCommand(
                    "Toggles the Perfect Clear Finder.",
                    e => {
                        Window?.SetPerfectClear(e).Wait();
                        return null;
                    },
                    () => $"pcf = {Preferences.PerfectClear.ToString().ToLower()}"
                )},

                {"ft", new ChatCommand<int>(
                    "Sets the amount of rounds to win the game.",
                    "rounds",
                    s => null,
                    e => {
                        if (e < 1)
                            return ChatCommandBase.InvalidParameters;

                        if (e > 15) e = 15;
                           
                        return $"/set match.ft={e}\nft = {e}";
                    }
                )},

                {"tls1", new ChatCommand(
                    "Loads Tetra League Season 1 settings.",
                    () => $"/set options.presets=tetra league (season 1)\nTetra League Season 1 settings loaded"
                )},

                {"tls2", new ChatCommand(
                    "Loads Tetra League Season 2 settings.",
                    () => $"/set options.presets=tetra league\nTetra League Season 2 settings loaded"
                )},

                {"grav", new OnOffChatCommand(
                    "Toggles gravity increase.",
                    e => $"/set options.gincrease={(e? 0.0035 : 0)}\ngravity = {e.ToString().ToLower()}"
                )},

                {"start", new ChatCommand(
                    "Starts the game.",
                    () => "/start\n#3000\nglhf"
                )},

                {"abort", new ChatCommand(
                    "Aborts a running game.",
                    () => "/abort\nGame aborted.",
                    true
                )}
            };
        }

        protected override void BeforeDispose() {
            server.Stop();
        }
    }
}
