using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameBoard : IGameBoard, IInitializable {
        [Inject]
        IGameRules GameRules;

        [Inject]
        GamePiece.Factory GamePieceFactory;

        [Inject]
        IMonsterManager MonsterManager;

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

        public void RandomizeGameBoardIfNoMonsterCombosAvailable() {
            bool doesBoardHaveAnyCombo = false;
            foreach ( IGameMonster monster in MonsterManager.CurrentMonsters ) {
                List<int> monsterCombo = monster.AttackCombo;
                if ( IsComboOnBoard( monsterCombo ) ) {
                    doesBoardHaveAnyCombo = true;
                    break;
                }
            }

            if ( !doesBoardHaveAnyCombo ) {
                RandomizeBoard();
                RandomizeGameBoardIfNoMonsterCombosAvailable();
            }
        }

        public bool IsComboOnBoard( List<int> i_combo ) {
            for ( int i = 0; i < BoardPieces.GetLength( 0 ); ++i ) {
                for ( int j = 0; j < BoardPieces.GetLength( 1 ); ++j ) {
                    List<IGamePiece> test = new List<IGamePiece>() ;
                    if ( SearchBoardForCombo(i, j, test, i_combo ) ) {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AreRowOrColumnOutOfBounds( int i_row, int i_col) {
            if ( i_row < 0 || i_row >= BoardPieces.GetLength( 0 ) || i_col < 0 || i_col >= BoardPieces.GetLength( 1 ) ) {
                return true;
            } else {
                return false;
            }
        }

        private bool IsValidPieceForChain( IGamePiece i_piece, List<IGamePiece> i_chainInProgress, List<int> i_targetChain ) {
            if ( i_chainInProgress.Contains( i_piece ) || i_piece.PieceType != i_targetChain[i_chainInProgress.Count] ) {
                return false;
            } else {
                return true;
            }
        }

        private bool SearchBoardForCombo( int i_row, int i_col, List<IGamePiece> i_chainInProgress, List<int> i_targetChain ) {
            if ( AreRowOrColumnOutOfBounds( i_row, i_col ) ) {
                return false;
            }

            IGamePiece onPiece = BoardPieces[i_row, i_col];
            if ( IsValidPieceForChain(onPiece, i_chainInProgress, i_targetChain ) ) {
                i_chainInProgress.Add( onPiece );
            } else {
                return false;
            }

            if ( i_chainInProgress.Count == i_targetChain.Count ) {                
                return true;    // entire combo was found!
            }

            // getting here means we have a match so far, but entire combo not yet found
            if ( SearchBoardForCombo( i_row+1, i_col, i_chainInProgress, i_targetChain ) ||
                SearchBoardForCombo(i_row-1, i_col, i_chainInProgress, i_targetChain ) ||
                SearchBoardForCombo(i_row, i_col+1, i_chainInProgress, i_targetChain ) ||
                SearchBoardForCombo(i_row,i_col-1, i_chainInProgress, i_targetChain ) ) {
                return true;
            } else {
                i_chainInProgress.Remove( onPiece );
                return false;
            }                     
        }

        public void InitBoardForTesting( IGamePiece[,] i_board ) {
            BoardPieces = i_board;
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
