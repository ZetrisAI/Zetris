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

    class RoomDataPayload {
        public Types.RoomData roomData { get; set; }
    }

    class AuthenticatedPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
    }

    class PlayerJoinedPayload {
        public Types.PlayerData playerData { get; set; }
    }

    class PlayerLeftPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
    }

    class PlayerBannedPayload {
        public Types.BotInfo botInfo { get; set; }
    }

    class PlayerUnbannedPayload {
        public Types.BotInfo botInfo { get; set; }
    }

    class SettingsChangedPayload {
        public Types.RoomData roomData { get; set; }
    }

    class RoundStartedPayload {
        public double startsAt { get; set; }
        public Types.RoomData roomData { get; set; }
    }

    class RequestMovePayload {
        public Types.GameState gameState { get; set; }
        public Types.PlayerData[] players { get; set; }
    }

    class PlayerActionPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
        /// <summary>
        /// type Command = "hold" | "move_left" | "move_right" | "sonic_left" | "sonic_right" | "rotate_cw" | "rotate_ccw" | "drop" | "sonic_drop" | "none";
        /// </summary>
        public string[] commands { get; set; }
        public Types.GameState gameState { get; set; }
        public Types.GameState prevGameState { get; set; }
        public Types.GameEvent[] events { get; set; }
    }

    class PlayerDamageReceivedPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
        public double damage { get; set; }
        public Types.GameState gameState { get; set; }
    }

    class RoundOverPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string winnerId { get; set; }
        public Types.BotInfo winnerInfo { get; set; }
        public Types.RoomData roomData { get; set; }
    }

    class GameOverPayload {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string winnerId { get; set; }
        public Types.BotInfo winnerInfo { get; set; }
        public Types.RoomData roomData { get; set; }
    }

    class GameResetPayload {
        public Types.RoomData roomData { get; set; }
    }

    class PingPayload {
        public double timestamp { get; set; }
    }

    class ActionPayload {
        /// <summary>
        /// type Command = "hold" | "move_left" | "move_right" | "sonic_left" | "sonic_right" | "rotate_cw" | "rotate_ccw" | "drop" | "sonic_drop" | "none";
        /// </summary>
        public string[] commands { get; set; }
    }
}
