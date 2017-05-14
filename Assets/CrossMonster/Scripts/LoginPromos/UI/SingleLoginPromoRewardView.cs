using MyLibrary;

namespace MonsterMatch {
    public class SingleLoginPromoRewardView : GroupView {
        private ISingleLoginPromoRewardPM mPM;

        public SingleRewardView RewardView;

        public void Init( ISingleLoginPromoRewardPM i_pm ) {
            mPM = i_pm;
            RewardView.Init( mPM.RewardPM );

            SetModel( mPM.ViewModel );
        }
    }
}