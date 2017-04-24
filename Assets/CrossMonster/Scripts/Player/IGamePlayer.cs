﻿
namespace MonsterMatch {
    public interface IGamePlayer {
        int GetAttackPowerForType( int i_type );
        int GetDefenseForType( int i_type );
        int HP { get; }

        void Dispose();
    }
}
