using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class MonsterPM : PresentationModel, IMonsterPM {
        public const string ID_PROPERTY = "Id";
        public const string HP_PROPERTY = "HP";

        private IGameMonster mMonster;
        public List<int> AttackCombo { get { return mMonster.AttackCombo; } }

        public MonsterPM( IGameMonster i_monster ) {
            mMonster = i_monster;

            SetIdProperty();
            SetHpProperty();
        }

        private void SetIdProperty() {
            ViewModel.SetProperty( ID_PROPERTY, mMonster.Id );
        }

        private void SetHpProperty() {
            ViewModel.SetProperty( HP_PROPERTY, mMonster.RemainingHP );
        }
    }
}
