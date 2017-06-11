using System.Collections.Generic;
using MyLibrary;

namespace MonsterMatch {
    public interface IDungeonGameSessionData {
        List<string> GetMonsters();
        List<IGameRewardData> GetRewards();

        int GetNumWaves();

        bool AllowDiagonals();
        bool StraightMovesOnly();
        bool ShouldRotatePieces();
    }
}
