using MyLibrary;
using UnityEngine;

namespace CrossMonsters {
    public class GamePiecePM : PresentationModel, IGamePiecePM {
        public const string PIECE_TYPE_PROPERTY = "PieceType";
        public const string BG_COLOR_PROPERTY = "BackgroundColor";

        public GamePiecePM( IGamePiece i_piece ) {
            SetPieceTypeProperty( i_piece );
            SetBackgroundColorProperty( Color.white ); // TODO make constant
        }

        private void SetPieceTypeProperty( IGamePiece i_piece ) {
            ViewModel.SetProperty( PIECE_TYPE_PROPERTY, i_piece.PieceType.ToString() );
        }

        private void SetBackgroundColorProperty( Color i_color ) {
            ViewModel.SetProperty( BG_COLOR_PROPERTY, i_color );
        }
    }
}
