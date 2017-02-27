using MyLibrary;
using System;
using UnityEngine;

namespace CrossMonsters {
    public class GamePlayer : IGamePlayer {
        private int mHP;
        public int HP { get { return mHP; } set { mHP = value; } }

        private IPlayerData mData;

        public GamePlayer( IPlayerData i_data ) {
            mData = i_data;
            SetHP( i_data );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGameMonster>( GameMessages.MONSTER_ATTACK, OnAttacked );
            } else {
                MyMessenger.Instance.RemoveListener<IGameMonster>( GameMessages.MONSTER_ATTACK, OnAttacked );
            }
        }

        public void OnAttacked( IGameMonster i_monster ) {
            int damageTaken = GetDamageFromMonster( i_monster );
            RemoveHP( damageTaken );
            SendUpdateHealthMessage();
        }

        public int GetDamageFromMonster( IGameMonster i_monster ) {
            int monsterAttack = i_monster.AttackPower;
            int playerDefense = GetDefenseForType( i_monster.AttackType );

            return Mathf.Max( 1, monsterAttack - playerDefense );
        }

        public int GetAttackPowerForType( int i_type ) {
            throw new NotImplementedException();
        }

        public int GetDefenseForType( int i_type ) {
            return mData.GetDefenseForType( i_type );
        }

        private void RemoveHP( int i_damage ) {
            HP -= i_damage;
        }

        private void SetHP( IPlayerData i_data ) {
            HP = i_data.GetHP();
        }

        private void SendUpdateHealthMessage() {
            MyMessenger.Instance.Send( GameMessages.UPDATE_PLAYER_HP );
        }
    }
}
