using System.Collections.Generic;

namespace CrossMonsters {
    public class DungeonGameSessionData : IDungeonGameSessionData {
        public List<string> Monsters;

        public bool AllowDiagonalMoves;
        public bool StraightLinesOnly;
        public bool ShouldRotatePiecesAfterUse;   

        public List<string> GetMonsters() {
            return Monsters;
        }

        public bool AllowDiagonals() {
            return AllowDiagonalMoves;
        }

        public bool StraightMovesOnly() {
            return StraightLinesOnly;
        }

        public bool ShouldRotatePieces() {
            return ShouldRotatePiecesAfterUse;
        }
    }
}
