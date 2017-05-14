using MyLibrary;

namespace MonsterMatch {
    public class SingleLoginPromoDisplayView : GroupView {        
        private ISingleLoginPromoDisplayPM mPM;

        public void Init( ISingleLoginPromoDisplayPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );
        }

        public void ClosePromo() {
            mPM.Hide();
        }
    }
}
