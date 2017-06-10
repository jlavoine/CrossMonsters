using MyLibrary;
using System;
using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class GamePlayer : IGamePlayer, IInitializable {        
        private IMessageService Messenger;
        private IDamageCalculator mDamageCalculator;
        private IPlayerDataManager mPlayerDataManager;
        private ICurrentBoostUnits mBoostUnits;

        private int mHP;
        public int HP { get { return mHP; } set { mHP = value; } }

        private int mMaxHP;
        public int MaxHP { get { return mMaxHP; } set { mMaxHP = value; } }

        public GamePlayer( IMessageService i_messenger, ICurrentBoostUnits i_boostUnits, IDamageCalculator i_damageCalculator, IPlayerDataManager i_playerDataManager ) {
            mDamageCalculator = i_damageCalculator;
            mPlayerDataManager = i_playerDataManager;
            mBoostUnits = i_boostUnits;
            Messenger = i_messenger;

            SetStartingHP();            
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
            AlterHP( -damageTaken );            
        }

        public void OnWaveFinished() {
            int hpRegen = GetHpRegenPerWave();
            AlterHP( hpRegen );
        }

        public int GetHpRegenPerWave() {
            int baseRegen = mPlayerDataManager.GetStat( PlayerStats.WAVE_HP_REGEN );
            int bonusRegen = mBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_HP_WAVE_REGEN );
            int totalRegen = baseRegen + bonusRegen;

            return totalRegen;
        }

        public int GetAttackPowerForType( int i_type ) {
            int bonusDamage = mBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_DAMAGE );
            int baseDamage = mPlayerDataManager.GetStat( PlayerStats.PHY_ATK );
            int totalDamage = bonusDamage + baseDamage;

            return totalDamage;
        }

        public int GetDefenseForType( int i_type ) {
            int bonusDefense = mBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_DEFENSE );
            int baseDefense = mPlayerDataManager.GetStat( PlayerStats.PHY_DEF );
            int totalDefense = bonusDefense + baseDefense;

            return totalDefense;
        }

        public void AlterHP( int i_hpChange ) {
            HP = HP + i_hpChange;

            if ( HP < 0 ) {
                HP = 0;
            } else if ( HP > MaxHP ) {
                HP = MaxHP;
            }

            SendUpdateHealthMessage();

            if ( IsDead() ) {
                SendPlayerDeadMessage();
            }            
        }

        private void SetStartingHP() {
            HP = mPlayerDataManager.GetStat( PlayerStats.HP );
            HP += mBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_HP );
            MaxHP = HP;
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
