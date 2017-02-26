
namespace CrossMonsters {
    public class GameBoard : IGameBoard {

        private IGamePiece[,] mBoard;
        public IGamePiece[,] BoardPieces { get { return mBoard; } set { mBoard = value; } }

        public GameBoard() {
            CreateBoard();
            FillBoard();
        }

        private void CreateBoard() {
            int size = GameRules.Instance.GetBoardSize();
            BoardPieces = new IGamePiece[size,size];
        }

        private void FillBoard() {
            for ( int i = 0; i < BoardPieces.GetLength(0); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength(1); ++j ) {
                    int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.Instance.GetPieceTypes() );
                    BoardPieces[i, j] = new GamePiece( randomPieceType );
                }
            }
        }
    }
}
