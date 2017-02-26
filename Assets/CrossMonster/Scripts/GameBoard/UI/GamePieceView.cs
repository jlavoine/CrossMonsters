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
            UnityEngine.Debug.LogError( "S1" );
            if ( ChainManager.Instance.IsNoChain() ) {
                UnityEngine.Debug.LogError( "S2" );
                ChainManager.Instance.StartChain( mPM.GamePiece );
            }
        }

        public void OnPointerEnter() {
            UnityEngine.Debug.LogError( "a" );
            if ( ChainManager.Instance.IsActiveChain() ) {
                UnityEngine.Debug.LogError( "b" );
                ChainManager.Instance.ContinueChain( mPM.GamePiece );
            }
        }
    }
}
