using MyLibrary;

namespace MonsterMatch {
    public class TimedChestView : GroupView {
        public SingleRewardView RewardView;

        private ITimedChestPM mPM;

        public void Init( ITimedChestPM i_pm ) {
            mPM = i_pm;
            RewardView.Init( i_pm.RewardPM );

            SetModel( mPM.ViewModel );
        }

        public void OpenChestClicked() {
            mPM.Open();
        }
    }
}