using System.Collections.Generic;

namespace CrossMonsters {
    public interface IDungeonGameSessionData {
        List<string> GetMonsters();
        List<IDungeonRewardData> GetRewards();

        bool AllowDiagonals();
        bool StraightMovesOnly();
        bool ShouldRotatePieces();
    }
}
