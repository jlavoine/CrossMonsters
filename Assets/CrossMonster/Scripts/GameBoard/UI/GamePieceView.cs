using MyLibrary;

namespace CrossMonsters {
    public class GamePieceView : GroupView {

        private IGamePiecePM mPM;

        public void Init( IGamePiecePM i_pm ) {
            mPM = i_pm;

            SetModel( i_pm.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        public void OnPointerDown() {
            if ( ChainManager.Instance.IsNoChain() ) {
                ChainManager.Instance.StartChain( mPM.GamePiece );
            }
        }

        public void OnPointerEnter() {
            if ( ChainManager.Instance.IsActiveChain() ) {
                ChainManager.Instance.ContinueChain( mPM.GamePiece );
            }
        }

        public void OnPointerUp() {
            if ( ChainManager.Instance.IsActiveChain() ) {
                ChainManager.Instance.EndChain();
            }
        }
    }
}
