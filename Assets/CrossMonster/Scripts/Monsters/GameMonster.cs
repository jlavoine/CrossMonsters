using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class GameMonster : BusinessModel, IGameMonster {
        private int mRemainingHP;
        private int mDefense;
        private int mDefenseType;
        public int mAttackPower;
        public int mAttackType;
        private long mAttackRate;
        private string mId;
        private List<int> mAttackCombo;

        public int RemainingHP { get { return mRemainingHP; } set { mRemainingHP = value; } }        
        public int Defense { get { return mDefense; } set { mDefense = value; } }        
        public int DefenseType { get { return mDefenseType; } set { mDefenseType = value; } }
        public int AttackPower { get { return mAttackPower; } set { mAttackPower = value; } }
        public int AttackType { get { return mAttackType; } set { mAttackType = value; } }
        public long AttackRate { get { return mAttackRate; } set { mAttackRate = value; } }
        public List<int> AttackCombo { get { return mAttackCombo; } set { mAttackCombo = value; } }
        public string Id { get { return mId; } set { mId = value; } }

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
            SendModelChangedEvent();
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

        public bool DoesMatchCombo( List<IGamePiece> i_combo ) {
            if ( i_combo == null || i_combo.Count != AttackCombo.Count ) {
                return false;
            }

            for ( int i = 0; i < AttackCombo.Count; ++i ) {
                if ( AttackCombo[i] != i_combo[i].PieceType ) {
                    return false;
                }
            }

            return true;
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
            AttackPower = i_data.GetDamage();
            AttackType = i_data.GetDamageType();
            AttackRate = i_data.GetAttackRate();
            AttackCombo = i_data.GetAttackCombo();
            Id = i_data.GetId();
        }
    }
}
