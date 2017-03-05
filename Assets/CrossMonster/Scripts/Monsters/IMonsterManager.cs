using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterManager {
        void Tick( long i_time );
        void ProcessPlayerMove( IGamePlayer i_player, List<IGamePiece> i_move );
        void SetMonsters( List<IGameMonster> i_monsters );

        bool DoesMoveMatchAnyCurrentMonsters( List<IGamePiece> i_move );

        List<IGameMonster> CurrentMonsters { get; }
        List<IGameMonster> RemainingMonsters { get; }
    }
}
