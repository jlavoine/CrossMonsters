using System.Collections.Generic;

namespace CrossMonsters {
    public interface IGameMonster {
        int RemainingHP { get; set; }

        void Tick( long i_time );
        void AttackedByPlayer( IGamePlayer i_player );

        bool DoesMatchCombo( List<int> i_combo );
    }
}
