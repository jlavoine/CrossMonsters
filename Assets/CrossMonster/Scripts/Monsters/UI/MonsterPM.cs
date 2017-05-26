using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class MonsterPM : PresentationModel, IMonsterPM {
        public const string ID_PROPERTY = "Id";
        public const string HP_PROPERTY = "HP";
        public const string ATTACK_PROGRESS_PROPERTY = "AttackProgress";
        public const string DESTROY_PROPERTY = "ShouldDestroy";

        private IGameMonster mMonster;
        public List<int> AttackCombo { get { return mMonster.AttackCombo; } }

        public MonsterPM( IGameMonster i_monster ) : base( i_monster ) {
            mMonster = i_monster;
            
            SetIdProperty();
            UpdateProperties();            
        }

        private void SetIdProperty() {
            ViewModel.SetProperty( ID_PROPERTY, mMonster.Id );
        }

        private void SetHpProperty() {
            ViewModel.SetProperty( HP_PROPERTY, mMonster.RemainingHP );
        }

        private void SetAttackProgressProperty() {
            ViewModel.SetProperty( ATTACK_PROGRESS_PROPERTY, mMonster.GetAttackProgress() );
        }

        private void SetShouldDestroyProperty() {
            bool shouldDestroy = mMonster.IsDead();
            ViewModel.SetProperty( DESTROY_PROPERTY, shouldDestroy );
        }

        private void UpdateProperties() {
            SetHpProperty();
            SetAttackProgressProperty();
            SetShouldDestroyProperty();
        }

        protected override void OnModelUpdated() {
            UpdateProperties();
        }
    }
}
