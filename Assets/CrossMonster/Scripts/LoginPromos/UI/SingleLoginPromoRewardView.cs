using MyLibrary;

namespace MonsterMatch {
    public class SingleLoginPromoRewardView : GroupView {
        private ISingleLoginPromoRewardPM mPM;

        public void Init( ISingleLoginPromoRewardPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );
        }
    }
}