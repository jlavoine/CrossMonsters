
namespace MyLibrary {
    public class ActiveLoginPromoButtonView : GroupView {

        private IActiveLoginPromoButtonPM mPM;

        public void Init( IActiveLoginPromoButtonPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );
        }
    }
}
