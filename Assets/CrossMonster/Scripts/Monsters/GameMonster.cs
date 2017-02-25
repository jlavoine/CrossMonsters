using UnityEngine;
using MyLibrary;

namespace CrossMonsters {
    public class GameMonster : IGameMonster {
        private int mRemainingHP;
        private int mDefense;
        private int mDefenseType;
        private long mAttackRate;

        public int RemainingHP { get { return mRemainingHP; } set { mRemainingHP = value; } }        
        public int Defense { get { return mDefense; } set { mDefense = value; } }        
        public int DefenseType { get { return mDefenseType; } set { mDefenseType = value; } }
        public long AttackRate { get { return mAttackRate; } set { mAttackRate = value; } }

        private float mAttackCycle;
        public float AttackCycle { get { return mAttackCycle; } set { mAttackCycle = value; } }

        public GameMonster( IMonsterData i_data ) {
            ResetAttackCycle();
            SetStats( i_data );
        }

        public void AttackedByPlayer( IGamePlayer i_player ) {
            int playerAttackPower = Mathf.Max( i_player.GetAttackPowerForType( DefenseType ), 0 );
            int damage = Mathf.Max( playerAttackPower - Defense, 1 );

            RemainingHP -= damage;
        }

        public void Tick( long i_time ) {
            if ( i_time < 0 ) {
                i_time = 0;
            }

            AddTimeToAttackCycle( i_time );            

            while ( AttackCycle >= AttackRate ) {
                PerformAttack();                
            }
        }

        private void AddTimeToAttackCycle( long i_time ) {
            AttackCycle += i_time;
        }

        private void PerformAttack() {
            RemoveAttackRateFromCycle();
            SendAttackMessage();
        }

        private void RemoveAttackRateFromCycle() {
            AttackCycle -= AttackRate;
        }

        private void SendAttackMessage() {
            MyMessenger.Instance.Send<IGameMonster>( GameMessages.MONSTER_ATTACK, this );
        }

        public bool IsDead() {
            return RemainingHP <= 0;
        }

        private void ResetAttackCycle() {
            AttackCycle = 0f;
        }

        private void SetStats( IMonsterData i_data ) {
            RemainingHP = i_data.GetMaxHP();
            Defense = i_data.GetDefense();
            DefenseType = i_data.GetDefenseType();
            AttackRate = i_data.GetAttackRate();
        }
    }
}
