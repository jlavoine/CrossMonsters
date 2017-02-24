using UnityEngine;

namespace CrossMonsters {
    public class GameMonster : IGameMonster {
        private int mRemainingHP;
        private int mDefense;
        private int mDefenseType;

        public int RemainingHP { get { return mRemainingHP; } set { mRemainingHP = value; } }        
        public int Defense { get { return mDefense; } set { mDefense = value; } }        
        public int DefenseType { get { return mDefenseType; } set { mDefenseType = value; } }

        public GameMonster( IMonsterData i_data ) {
            SetStats( i_data );
        }

        public void AttackedByPlayer( IGamePlayer i_player ) {
            int playerAttackPower = Mathf.Max( i_player.GetAttackPowerForType( DefenseType ), 0 );
            int damage = Mathf.Max( playerAttackPower - Defense, 1 );

            RemainingHP -= damage;
        }

        public bool IsDead() {
            return RemainingHP <= 0;
        }

        private void SetStats( IMonsterData i_data ) {
            RemainingHP = i_data.GetMaxHP();
            Defense = i_data.GetDefense();
            DefenseType = i_data.GetDefenseType();
        }
    }
}
