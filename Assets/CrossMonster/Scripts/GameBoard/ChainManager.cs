using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainManager : IChainManager {
        private List<IGamePiece> mChain = null;
        public List<IGamePiece> Chain { get { return mChain; } set { mChain = value; } }

        public ChainManager() {
            UnityEngine.Debug.LogError( "Initing!" );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {                
            } else {
            }
        }

        public void StartChain( IGamePiece i_piece ) {
            if ( IsNoChain() ) {
                CreateChain();
                AddPieceToChain( i_piece );
                SendPieceAddedEvent( i_piece );
            }
        }

        public void ContinueChain( IGamePiece i_piece ) {
            if ( IsActiveChain() && !IsPieceInChain( i_piece ) ) {
                AddPieceToChain( i_piece );
                SendPieceAddedEvent( i_piece );
            }
        }

        public void EndChain() {
            if ( IsActiveChain() ) {
                ResetChain();
                SendChainResetEvent();
            }
        }

        public void CancelChain() {
            if ( IsActiveChain() ) {
                ResetChain();
                SendChainResetEvent();
            }
        }

        public bool IsNoChain() {
            return mChain == null;
        }

        public bool IsActiveChain() {
            return mChain != null;
        }

        private void CreateChain() {
            mChain = new List<IGamePiece>();
        }

        private void ResetChain() {
            mChain = null;
        }

        private void AddPieceToChain( IGamePiece i_piece ) {
            mChain.Add( i_piece );
        }

        private void SendPieceAddedEvent( IGamePiece i_piece ) {
            MyMessenger.Instance.Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, i_piece );
        }

        private void SendChainResetEvent() {
            MyMessenger.Instance.Send( GameMessages.CHAIN_RESET );
        }

        private bool IsPieceInChain( IGamePiece i_piece ) {
            return Chain.Contains( i_piece );
        }
    }
}
