using System.Collections.Generic;

namespace CrossMonsters {
    public interface IDungeonGameSessionData {
        List<string> GetMonsters();

        bool AllowDiagonals();
        bool StraightMovesOnly();
        bool ShouldRotatePieces();
    }
}
