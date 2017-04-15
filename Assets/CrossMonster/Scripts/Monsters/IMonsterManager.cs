using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterManager {
        void Tick( long i_time );
        void ProcessPlayerMove( IGamePlayer i_player, List<IGamePiece> i_move );
        void SetMonsters( List<IMonsterWaveData> i_monsters );

        bool DoesMoveMatchAnyCurrentMonsters( List<IGamePiece> i_move );

        IMonsterWave CurrentWave { get; }
        List<IMonsterWave> RemainingWaves { get; }

        List<List<int>> GetCurrentMonsterCombos();
    }
}
