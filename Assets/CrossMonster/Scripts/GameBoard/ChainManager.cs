using MyLibrary;
using System.Collections.Generic;

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

        private List<IGamePiece> mChain = null;
        public List<IGamePiece> Chain { get { return mChain; } set { mChain = value; } }

        public ChainManager() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGamePiece>( GameMessages.START_CHAIN, StartChain );
            } else {
                MyMessenger.Instance.RemoveListener<IGamePiece>( GameMessages.START_CHAIN, StartChain );
            }
        }

        public void StartChain( IGamePiece i_piece ) {
            if ( IsNoCurrentChain() ) {
                CreateChain();
                //AddPieceToChain( i_piece );
                //SendPieceAddedEvent( i_piece );
            }
        }

        public bool IsNoCurrentChain() {
            return mChain == null;
        }

        private void CreateChain() {
            mChain = new List<IGamePiece>();
        }

        private void AddPieceToChain( IGamePiece i_piece ) {
            mChain.Add( i_piece );
        }

        private void SendPieceAddedEvent( IGamePiece i_piece ) {
            MyMessenger.Instance.Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, i_piece );
        }
    }
}
