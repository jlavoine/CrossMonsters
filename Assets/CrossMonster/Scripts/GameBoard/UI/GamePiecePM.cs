using MyLibrary;
using UnityEngine;

namespace CrossMonsters {
    public class GamePiecePM : PresentationModel, IGamePiecePM {
        public const string PIECE_TYPE_PROPERTY = "PieceType";
        public const string BG_COLOR_PROPERTY = "BackgroundColor";

        private IGamePiece mPiece;
        public IGamePiece GamePiece { get { return mPiece; } set { mPiece = value; } }

        public GamePiecePM( IGamePiece i_piece ) : base( i_piece ) {
            GamePiece = i_piece;

            ListenForMessages( true );

            UpdateProperties();            
            ResetColorBackgroundPropertyToDefault();            
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, OnPieceAddedToChain );
                MyMessenger.Instance.AddListener( GameMessages.CHAIN_RESET, OnChainReset );
            } else {
                MyMessenger.Instance.RemoveListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, OnPieceAddedToChain );
                MyMessenger.Instance.RemoveListener( GameMessages.CHAIN_RESET, OnChainReset );
            }
        }

        public void OnPieceAddedToChain( IGamePiece i_piece ) {
            if ( GamePiece == i_piece ) {
                SetBackgroundColorProperty( Color.yellow ); // TODO make constant
            }
        }

        public void OnChainReset() {
            ResetColorBackgroundPropertyToDefault();
        }

        protected override void OnModelUpdated() {
            UpdateProperties();
        }

        private void UpdateProperties() {
            SetPieceTypeProperty();
        }

        private void SetPieceTypeProperty() {
            ViewModel.SetProperty( PIECE_TYPE_PROPERTY, GamePiece.PieceType.ToString() );
        }

        private void SetBackgroundColorProperty( Color i_color ) {
            ViewModel.SetProperty( BG_COLOR_PROPERTY, i_color );
        }

        private void ResetColorBackgroundPropertyToDefault() {
            SetBackgroundColorProperty( Color.white ); // TODO make constant
        }
    }
}
