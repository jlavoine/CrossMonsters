
namespace CrossMonsters {
    public class GameBoard : IGameBoard {

        private IGamePiece[,] mBoard;
        public IGamePiece[,] Board { get { return mBoard; } set { mBoard = value; } }

        public GameBoard() {
            CreateBoard();
            FillBoard();
        }

        private void CreateBoard() {
            int size = GameRules.Instance.GetBoardSize();
            Board = new IGamePiece[size,size];
        }

        private void FillBoard() {
            for ( int i = 0; i < Board.GetLength(0); ++i ) {
                for ( int j = 0; j < Board.GetLength(1); ++j ) {
                    int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.Instance.GetPieceTypes() );
                    Board[i, j] = new GamePiece( randomPieceType );
                }
            }
        }
    }
}
