using MyLibrary;

namespace CrossMonsters {
    public class GamePiecePM : PresentationModel, IGamePiecePM {
        public const string PIECE_TYPE_PROPERTY = "PieceType";

        public GamePiecePM( IGamePiece i_piece ) {
            SetPieceTypeProperty( i_piece );
        }

        private void SetPieceTypeProperty( IGamePiece i_piece ) {
            ViewModel.SetProperty( PIECE_TYPE_PROPERTY, i_piece.PieceType.ToString() );
        }
    }
}
