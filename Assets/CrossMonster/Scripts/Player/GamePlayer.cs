
using System;

namespace CrossMonsters {
    public class GamePlayer : IGamePlayer {
        private int mHP;
        public int HP { get { return mHP; } set { mHP = value; } }

        public GamePlayer( IPlayerData i_data ) {
            SetHP( i_data );
        }

        public int GetAttackPowerForType( int i_type ) {
            throw new NotImplementedException();
        }

        private void SetHP( IPlayerData i_data ) {
            HP = i_data.GetHP();
        }
    }
}
