using System.Collections.Generic;
using MyLibrary;

namespace MonsterMatch {
    public interface IDungeonGameSessionData {
        string GetGameMode();

        List<string> GetMonsters();
        List<IGameRewardData> GetRewards();

        int GetNumWaves();

        bool AllowDiagonals();
        bool StraightMovesOnly();
        bool ShouldRotatePieces();
    }
}
