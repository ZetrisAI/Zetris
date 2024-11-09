using Newtonsoft.Json.Linq;

namespace Zetris.BotrisBattle.Messages {
    class Message {
        /// <summary>
        /// "room_data" | "authenticated" | "error" | "player_joined" | "player_left" | "player_banned" | "player_unbanned" | "settings_changed" | "game_started" | "round_started" | "request_move" | "player_action" | "player_damage_received" | "round_over" | "game_over" | "game_reset" | "ping"
        /// </summary>
        public string type { get; set; }
        public JToken payload { get; set; }

        public static Message Create(string type, object payload)
            => new Message() {
                type = type,
                payload = JToken.FromObject(payload)
            };
    }

    class AuthenticatedPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
    }

    class RoundStartedPayload {
        public Types.RoomData roomData { get; set; }
    }

    class RequestMovePayload {
        public Types.GameState gameState { get; set; }
    }

    class PlayerActionPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
        public Types.GameState gameState { get; set; }
        public Types.GameEvent[] events { get; set; }
    }

    class ActionPayload {
        /// <summary>
        /// type Command = "hold" | "move_left" | "move_right" | "sonic_left" | "sonic_right" | "rotate_cw" | "rotate_ccw" | "drop" | "sonic_drop" | "none";
        /// </summary>
        public string[] commands { get; set; }
    }
}
