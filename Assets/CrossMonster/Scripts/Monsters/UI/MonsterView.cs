﻿using MyLibrary;
using UnityEngine;

namespace CrossMonsters {
    public class MonsterView : GroupView {
        private IMonsterPM mPM;

        public void Init( IMonsterPM i_pm ) {
            mPM = i_pm;
            SetModel( mPM.ViewModel );

            InitAttackComboView();
        }

        private void InitAttackComboView() {
            AttackComboView comboView = gameObject.GetComponentInChildren<AttackComboView>();
            comboView.Init( mPM.AttackCombo );
        }
    }
}
