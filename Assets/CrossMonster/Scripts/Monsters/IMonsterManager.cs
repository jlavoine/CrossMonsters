using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterManager {
        void Init( List<IGameMonster> i_allMonsters );
        void Tick( long i_time );
        void ProcessPlayerMove( IGamePlayer i_player, List<int> i_move );

        List<IGameMonster> CurrentMonsters { get; }
    }
}
