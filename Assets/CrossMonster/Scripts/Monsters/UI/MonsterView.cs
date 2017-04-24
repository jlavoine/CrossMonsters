using MyLibrary;
using UnityEngine;

namespace MonsterMatch {
    public class MonsterView : GroupView {
        private IMonsterPM mPM;

        public void Init( IMonsterPM i_pm ) {
            mPM = i_pm;
            SetModel( mPM.ViewModel );

            InitAttackComboView();
        }

        protected override void OnDestroy() {
            mPM.Dispose();
        }

        private void InitAttackComboView() {
            AttackComboView comboView = gameObject.GetComponentInChildren<AttackComboView>();
            comboView.Init( mPM.AttackCombo );
        }
    }
}
