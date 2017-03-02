using MyLibrary;
using System;
using UnityEngine;
using Zenject;

namespace CrossMonsters {
    public class GamePlayer : IGamePlayer, IInitializable {
        [Inject]
        IMessageService Messenger;

        private IDamageCalculator mDamageCalculator;

        private int mHP;
        public int HP { get { return mHP; } set { mHP = value; } }

        private IPlayerData mData;

        public GamePlayer( IPlayerData i_data, IDamageCalculator i_damageCalculator ) {
            mDamageCalculator = i_damageCalculator;

            mData = i_data;
            SetHP( i_data );            
        }

        public void Initialize() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                Messenger.AddListener<IGameMonster>( GameMessages.MONSTER_ATTACK, OnAttacked );
            } else {
                Messenger.RemoveListener<IGameMonster>( GameMessages.MONSTER_ATTACK, OnAttacked );
            }
        }

        public void OnAttacked( IGameMonster i_monster ) {
            int damageTaken = mDamageCalculator.GetDamageFromMonster( i_monster, this );
            RemoveHP( damageTaken );
            SendUpdateHealthMessage();
        }

        public int GetAttackPowerForType( int i_type ) {
            return 10;
        }

        public int GetDefenseForType( int i_type ) {
            return mData.GetDefenseForType( i_type );
        }

        public void RemoveHP( int i_damage ) {
            HP = Mathf.Max( 0, HP - i_damage );

            if ( IsDead() ) {
                SendPlayerDeadMessage();
            }
        }

        private void SetHP( IPlayerData i_data ) {
            HP = i_data.GetHP();
        }

        private bool IsDead() {
            return HP <= 0;
        }

        private void SendUpdateHealthMessage() {
            Messenger.Send( GameMessages.UPDATE_PLAYER_HP );
        }

        private void SendPlayerDeadMessage() {
            Messenger.Send( GameMessages.PLAYER_DEAD );
        }
    }
}
