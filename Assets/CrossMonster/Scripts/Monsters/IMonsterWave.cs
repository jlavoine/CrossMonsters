using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterWave {
        List<IGameMonster> CurrentMonsters { get; }
        List<IGameMonster> RemainingMonsters { get; }

        void Tick( long i_time );
        void ProcessPlayerMove( IGamePlayer i_player, List<IGamePiece> i_move );
        void Prepare();

        List<List<int>> GetCurrentMonsterCombos();

        bool DoesMoveMatchAnyCurrentMonsters( List<IGamePiece> i_move );
        bool IsCleared();
    }
}
