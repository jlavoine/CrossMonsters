using System.Collections.Generic;
using MyLibrary;

namespace CrossMonsters {
    public interface IGameMonster : IBusinessModel {
        string Id { get; }
        int RemainingHP { get; set; }
        int AttackPower { get; set; }
        int AttackType { get; set; }
        List<int> AttackCombo { get; set; }

        void Tick( long i_time );
        void AttackedByPlayer( IGamePlayer i_player );

        bool DoesMatchCombo( List<IGamePiece> i_combo );
        bool IsDead();
    }
}
