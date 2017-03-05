using Zenject;

namespace CrossMonsters {
    public class GameBoard : IGameBoard, IInitializable {
        [Inject]
        IGameRules GameRules;

        [Inject]
        GamePiece.Factory GamePieceFactory;

        private IGamePiece[,] mBoard;
        public IGamePiece[,] BoardPieces { get { return mBoard; } set { mBoard = value; } }

        public GameBoard() {}

        public void Initialize() {
            CreateBoard();
            FillBoard();
        }

        public void RandomizeBoard() {
            for ( int i = 0; i < BoardPieces.GetLength( 0 ); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength( 1 ); ++j ) {
                    IGamePiece piece = BoardPieces[i, j];
                    piece.Randomize();
                }
            }
        }

        private void CreateBoard() {
            int size = GameRules.GetBoardSize();
            BoardPieces = new IGamePiece[size,size];
        }

        private void FillBoard() {
            for ( int i = 0; i < BoardPieces.GetLength(0); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength(1); ++j ) {
                    int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.GetPieceTypes() );
                    BoardPieces[i, j] = GamePieceFactory.Create( randomPieceType );
                }
            }
        }
    }
}
