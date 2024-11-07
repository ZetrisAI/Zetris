using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Zetris.BotrisBattle.Types {
    class RoomSettings {
        [JsonProperty("private")]
        public bool Private { get; set; }
        public double ft { get; set; }
        public double pps { get; set; }
        public double initialMultiplier { get; set; }
        public double finalMultiplier { get; set; }
        public double startMargin { get; set; }
        public double endMargin { get; set; }
    }

    class RoomData { 
        public string id { get; set; }
        public RoomSettings settings { get; set; }
        public bool gameOngoing { get; set; }
        public bool roundOngoing { get; set; }
        public double? startedAt { get; set; }
        public double? endedAt { get; set; }
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string lastWinner { get; set; }
        public PlayerData[] players { get; set; }
        public BotInfo[] banned { get; set; }
    }

    class PlayerData {
        /// <summary>
        /// type SessionId = string;
        /// </summary>
        public string sessionId { get; set; }
        public bool playing { get; set; }
        public BotInfo info { get; set; }
        public double wins { get; set; }
        public GameState gameState { get; set; }
    }

    class BotInfo {
        public string id { get; set; }
        public string name { get; set; }
        public string[][] avatar { get; set; }
        public string team { get; set; }
        public string language { get; set; }
        public string eval { get; set; }
        public string movegen { get; set; }
        public string search { get; set; }
        public Developer[] developers { get; set; }
    }

    class PieceData {
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// </summary>
        public string piece { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int rotation { get; set; }
    }

    class GarbageLine {
        public double delay { get; set; }
    }

    class GameState {
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// type Block = Piece | "G" | null;
        /// </summary>
        public string[][] board { get; set; }
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
        public bool canHold { get; set; }
        public double combo { get; set; }
        public bool b2b { get; set; }
        public double score { get; set; }
        public double piecesPlaced { get; set; }
        public bool dead { get; set; }
    }

    class GameEvent {
        /// <summary>
        /// "piece_placed" | "damage_tanked" | "clear" | "game_over";
        /// </summary>
        public string type { get; set; }
        public JToken payload { get; set; }
    }

    class PiecePlacedGameEventPayload {
        public PieceData initial { get; set; }
        public PieceData final { get; set; }
    }

    class DamageTankedGameEventPayload {
        public double[] holeIndices { get; set; }
    }

    class ClearGameEventPayload {
        /// <summary>
        /// type ClearName = "Single" | "Triple" | "Double" | "Quad" | "Perfect Clear" | "All-Spin Single" | "All-Spin Double" | "All-Spin Triple";
        /// </summary>
        public string clearName { get; set; }
        public bool allSpin { get; set; }
        public bool b2b { get; set; }
        public double combo { get; set; }
        public bool pc { get; set; }
        public double attack { get; set; }
        public double cancelled { get; set; }
        public PieceData piece { get; set; }
        public ClearedLine[] clearedLines { get; set; }
    }

    class ClearedLine {
        public double height { get; set; }
        /// <summary>
        /// type Piece = "I" | "O" | "J" | "L" | "S" | "Z" | "T";
        /// type Block = Piece | "G" | null;
        /// </summary>1
        public string[] blocks { get; set; }
    }

    class Developer {
        public string id { get; set; }
        public string displayName { get; set; }
    }
}
