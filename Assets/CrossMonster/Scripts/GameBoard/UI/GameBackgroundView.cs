using MyLibrary;

namespace CrossMonsters {
    public class GameBackgroundView : GroupView {
        private GameBackgroundPM mPM;

        void Start() {
            mPM = new GameBackgroundPM();

            SetModel( mPM.ViewModel );
        }

        public void OnPointerEnter() {
            if ( ChainManager.Instance.IsActiveChain() ) {
                mPM.PlayerEnteredBackground();
            }
        }
    }
}
