using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;

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
        protected override int RushTime() => 40;
        protected override bool Danger() => false; // there is no PUBLIC version of BotrisBattle bot

        string self = null;

        public string Token = null;
        public string RoomKey = null;

        WebSocket ws = null;
        bool wasClosed = false;

        Dictionary<string, Action<JToken>> handlers;

        BlockingCollection<Message> msgq = new BlockingCollection<Message>();

        protected override void LoopIteration() {
            Message msg;

            try {
                msg = msgq.Take();

            } catch (InvalidOperationException) {
                // queue is complete
                return;
            }

            handlers[msg.type](msg.payload);
        }

        protected override void Starting() {
            if (string.IsNullOrWhiteSpace(Token))
                throw new Exception("Token is not set");

            if (string.IsNullOrWhiteSpace(RoomKey))
                throw new Exception("RoomKey is not set");

            ws = new WebSocket($"wss://botrisbattle.com/ws?token={Token}&roomKey={RoomKey}");
            ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;

            ws.OnMessage += (s, e) => {
                Trace.WriteLine("RECV\n" + JToken.Parse(e.Data).ToString(Formatting.Indented));
                msgq.Add(JsonConvert.DeserializeObject<Message>(e.Data));
            };

            ws.OnClose += (s, e) => {
                if (!wasClosed)
                    throw new Exception("WebSocket closed unexpectedly");
            };

            handlers = new Dictionary<string, Action<JToken>> {
                {"room_data", payload => {
                    var roomData = payload.ToObject<RoomDataPayload>().roomData;
                }},

                {"authenticated", payload => {
                    self = payload.ToObject<AuthenticatedPayload>().sessionId;
                }},

                {"error", payload => {
                    var error = payload.ToObject<string>();
                }},

                {"player_joined", payload => {
                    var playerData = payload.ToObject<PlayerJoinedPayload>().playerData;
                }},

                {"player_left", payload => {
                    var sessionId = payload.ToObject<PlayerLeftPayload>().sessionId;
                }},

                {"player_banned", payload => {
                    var botInfo = payload.ToObject<PlayerBannedPayload>().botInfo;
                }},

                {"player_unbanned", payload => {
                    var botInfo = payload.ToObject<PlayerUnbannedPayload>().botInfo;
                }},

                {"settings_changed", payload => {
                    var roomData = payload.ToObject<SettingsChangedPayload>().roomData;
                }},

                {"game_started", _ => {
                }},

                {"round_started", payload => {
                    var data = payload.ToObject<RoundStartedPayload>();
                    var gameState = data.roomData.players.Single(i => i.sessionId == self).gameState;
                    // start thinking
                }},

                {"request_move", payload => {
                    var data = payload.ToObject<RequestMovePayload>();
                    // make decision and continue thinking
                }},

                {"player_action", payload => {
                    var data = payload.ToObject<PlayerActionPayload>();
                }},

                {"player_damage_received", payload => {
                    var data = payload.ToObject<PlayerDamageReceivedPayload>();
                }},

                {"round_over", payload => {
                    var data = payload.ToObject<RoundOverPayload>();
                }},

                {"game_over", payload => {
                    var data = payload.ToObject<GameOverPayload>();
                }},

                {"game_reset", payload => {
                    var roomData = payload.ToObject<RoomDataPayload>().roomData;
                }},

                {"ping", payload => {
                    var timestamp = payload.ToObject<PingPayload>().timestamp;
                }}
            };

            ws.Connect();
        }

        protected override void BeforeDispose() {
            wasClosed = true;
            ws?.Close();
        }
    }
}
