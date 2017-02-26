using MyLibrary;

namespace CrossMonsters {
    public class ChainManager : IChainManager {
        private static IChainManager mInstance;
        public static IChainManager Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new ChainManager();
                }
                return mInstance;
            }
            set {
                // tests only!
                mInstance = value;
            }
        }

        public ChainManager() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGamePiece>( GameMessages.START_CHAIN, OnStartChain );
            } else {
                MyMessenger.Instance.RemoveListener<IGamePiece>( GameMessages.START_CHAIN, OnStartChain );
            }
        }

        public void OnStartChain( IGamePiece i_piece ) {

        }
    }
}
