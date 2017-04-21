using MyLibrary;
using System;
using UnityEngine;
using Zenject;

namespace CrossMonsters {
    public class GamePlayer : IGamePlayer, IInitializable {
        [Inject]
        IMessageService Messenger;

        private IDamageCalculator mDamageCalculator;
        private IPlayerDataManager mPlayerDataManager;

        private int mHP;
        public int HP { get { return mHP; } set { mHP = value; } }

        public GamePlayer( IDamageCalculator i_damageCalculator, IPlayerDataManager i_playerDataManager ) {
            mDamageCalculator = i_damageCalculator;
            mPlayerDataManager = i_playerDataManager;

            SetHP();            
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
            return mPlayerDataManager.GetStat( PlayerStats.PHY_ATK );
        }

        public int GetDefenseForType( int i_type ) {
            return mPlayerDataManager.GetStat( PlayerStats.PHY_DEF );
        }

        public void RemoveHP( int i_damage ) {
            HP = Mathf.Max( 0, HP - i_damage );

            if ( IsDead() ) {
                SendPlayerDeadMessage();
            }
        }

        private void SetHP() {
            HP = mPlayerDataManager.GetStat( PlayerStats.HP );
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
