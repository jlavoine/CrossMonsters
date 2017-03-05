using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GamePieceView : GroupView {
        [Inject]
        IChainBuilder ChainManager;

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
            if ( ChainManager.IsNoChain() ) {
                ChainManager.StartChain( mPM.GamePiece );
            }
        }

        public void OnPointerEnter() {
            if ( ChainManager.IsActiveChain() ) {
                ChainManager.ContinueChain( mPM.GamePiece );
            }
        }

        public void OnPointerUp() {
            if ( ChainManager.IsActiveChain() ) {
                ChainManager.EndChain();
            }
        }
    }
}
