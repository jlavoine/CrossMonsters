using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class GamePieceView : GroupView {
        [Inject]
        IChainBuilder ChainManager;

        [Inject]
        IGameManager GameManager;

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
            if ( ChainManager.IsNoChain() && GameManager.IsGamePlaying() && mPM.GamePiece.IsSelectable() ) {
                ChainManager.StartChain( mPM.GamePiece );
            }
        }

        public void OnPointerEnter() {
            if ( ChainManager.IsActiveChain() && GameManager.IsGamePlaying() ) {
                ChainManager.ContinueChain( mPM.GamePiece );
            }
        }

        public void OnPointerUp() {
            if ( ChainManager.IsActiveChain() && GameManager.IsGamePlaying() ) {
                ChainManager.EndChain();
            }
        }

        public void OnAnimationComplete() {
            mPM.OnAnimationComplete();
        }
    }
}
