﻿using MyLibrary;

namespace MonsterMatch {
    public class SingleRewardView : GroupView {
        private ISingleRewardPM mPM;

        public void Init( ISingleRewardPM i_pm ) {
            mPM = i_pm;

            SetModel( i_pm.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        public void RewardCoverClicked() {
            mPM.UncoverReward();
        }
    }
}
