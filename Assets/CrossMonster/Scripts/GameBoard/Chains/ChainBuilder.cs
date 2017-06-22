using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class ChainBuilder : IChainBuilder {
        [Inject]
        IChainProcessor ChainProcessor;

        [Inject]
        IChainValidator ChainValidator;

        [Inject]
        IAudioManager Audio;

        [Inject]
        IMessageService MyMessenger;

        private List<IGamePiece> mChain = null;
        public List<IGamePiece> Chain { get { return mChain; } set { mChain = value; } }

        public ChainBuilder() {
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
            if ( IsActiveChain() && ChainValidator.IsValidPieceInChain( i_piece, mChain ) ) {
                AddPieceToChain( i_piece );
                SendPieceAddedEvent( i_piece );
            } else {
                CancelChain();
            }
        }

        public void EndChain() {
            if ( IsActiveChain() ) {
                ChainProcessor.Process( mChain );
                ResetChain();
                SendChainResetEvent();
            }
        }

        public void CancelChain() {
            if ( IsActiveChain() ) {
                MarkPiecesIncorrect( mChain );
                ResetChain();
                SendChainResetEvent();                          
                Audio.PlayOneShot( CombatAudioKeys.CHAIN_BROKEN );
            }
        }

        public void AddPieceToChain( IGamePiece i_piece ) {
            mChain.Add( i_piece );
            Audio.PlayOneShot( CombatAudioKeys.ADD_TO_CHAIN );
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

        private void MarkPiecesIncorrect( List<IGamePiece> i_pieces ) {
            if ( i_pieces != null ) {
                foreach ( IGamePiece piece in i_pieces ) {
                    piece.PieceFailedMatch();
                }
            }
        }

        private void SendPieceAddedEvent( IGamePiece i_piece ) {
            MyMessenger.Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, i_piece );
        }

        private void SendChainResetEvent() {
            MyMessenger.Send(GameMessages.CHAIN_RESET);
        }
    }
}
