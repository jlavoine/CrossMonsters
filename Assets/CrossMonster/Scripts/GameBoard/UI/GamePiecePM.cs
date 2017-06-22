using MyLibrary;
using UnityEngine;

namespace MonsterMatch {
    public class GamePiecePM : PresentationModel, IGamePiecePM {
        public const string PIECE_TYPE_PROPERTY = "PieceType";
        public const string IS_ON_PROPERTY = "IsOn";
        public const string TRIGGER_PROPERTY = "GamePieceOn_";
        public const string CHAIN_DROPPED_TRIGGER = "ChainDropped";
        public const string CHAIN_COMPLETE_TRIGGER = "ChainComplete";

        private IGamePiece mPiece;
        public IGamePiece GamePiece { get { return mPiece; } set { mPiece = value; } }

        public GamePiecePM( IGamePiece i_piece ) : base( i_piece ) {
            GamePiece = i_piece;

            ListenForMessages( true );

            UpdateProperties();
            ResetIsOnProperty();
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, OnPieceAddedToChain );
            } else {
                MyMessenger.Instance.RemoveListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, OnPieceAddedToChain );
            }
        }

        public void OnPieceAddedToChain( IGamePiece i_piece ) {
            if ( GamePiece == i_piece ) {
                SetIsOnProperty( true );
            }
        }

        public void OnAnimationComplete() {
            ResetIsOnProperty();
            GamePiece.State = GamePieceStates.Selectable;
            UpdateProperties();
        }

        public string GetAnimState( GamePieceStates i_state ) {
            switch ( i_state ) {
                case GamePieceStates.Correct:
                    return CHAIN_COMPLETE_TRIGGER;
                case GamePieceStates.Incorrect:
                    return CHAIN_DROPPED_TRIGGER;
                default:
                    return string.Empty;
            }
        }

        protected override void OnModelUpdated() {
            UpdateProperties();        
        }

        private void UpdateProperties() {
            SetPieceTypeProperty();
            SetPieceStateProperty();
        }

        private void SetPieceTypeProperty() {
            ViewModel.SetProperty( PIECE_TYPE_PROPERTY, GamePiece.PieceType.ToString() );
        }

        private void SetPieceStateProperty() {
            string state = GetAnimState( GamePiece.State );

            if ( !string.IsNullOrEmpty( state ) ) {
                ViewModel.SetProperty( TRIGGER_PROPERTY + GamePiece.PieceType, state );
            }
        }

        private void SetIsOnProperty( bool i_on ) {
            ViewModel.SetProperty( IS_ON_PROPERTY, i_on );
        }

        private void ResetIsOnProperty() {
            SetIsOnProperty( false );
        }
    }
}
