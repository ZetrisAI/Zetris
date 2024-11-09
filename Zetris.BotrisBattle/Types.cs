using Newtonsoft.Json.Linq;

namespace Zetris.BotrisBattle.Types {
    class RoomSettings {
        public double pps { get; set; }
    }

    class RoomData { 
        public RoomSettings settings { get; set; }
        public PlayerData[] players { get; set; }
    }

    class PlayerData {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
        public GameState gameState { get; set; }
    }

    class PieceData {
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string piece { get; set; }
    }

    class GarbageLine {}

    class GameState {
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// type Block = Piece | "G" | null;
        /// </summary>
        public string[][] board { get; set; }
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string[] bag { get; set; }
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string[] queue { get; set; }
        public GarbageLine[] garbageQueued { get; set; }
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string held { get; set; }
        public PieceData current { get; set; }
        public int combo { get; set; }
    }

    class GameEvent {
        /// <summary>
        /// "queue_added" | "piece_placed" | "damage_tanked" | "clear" | "game_over";
        /// </summary>
        public string type { get; set; }
        public JToken payload { get; set; }
    }

    class QueueAddedGameEventPayload {
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string piece { get; set; }
    }
}
