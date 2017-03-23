using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameBoard : IGameBoard, IInitializable {
        [Inject]
        IGameRules GameRules;

        [Inject]
        IValidBoardChecker BoardRandomizer;

        [Inject]
        GamePiece.Factory GamePieceFactory;

        private IGamePiece[,] mBoard;
        public IGamePiece[,] BoardPieces { get { return mBoard; } set { mBoard = value; } }

        public GameBoard() {}

        public void Initialize() {
            CreateBoard();
            FillBoard();
        }        
     
        public void InitBoardForTesting( IGamePiece[,] i_board ) {
            BoardPieces = i_board;
        }

        public void RandomizeGameBoardIfNoMonsterCombosAvailable() {
            while ( !BoardRandomizer.IsMonsterComboAvailableOnBoard( BoardPieces ) ) {
                RandomizeBoard();
            }
        }

        private void CreateBoard() {
            int size = GameRules.GetBoardSize();
            BoardPieces = new IGamePiece[size,size];
        }

        private void FillBoard() {
            int pieceIndex = 0;
            for ( int i = 0; i < BoardPieces.GetLength(0); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength(1); ++j ) {
                    int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.GetPieceTypes() );
                    BoardPieces[i, j] = GamePieceFactory.Create( randomPieceType, pieceIndex );
                    pieceIndex++;
                }
            }
        }

        private void RandomizeBoard() {
            for ( int i = 0; i < BoardPieces.GetLength( 0 ); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength( 1 ); ++j ) {
                    IGamePiece piece = BoardPieces[i, j];
                    piece.Randomize();
                }
            }
        }
    }
}
