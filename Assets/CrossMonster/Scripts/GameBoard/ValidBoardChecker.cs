using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class ValidBoardChecker : IValidBoardChecker {
        [Inject]
        IMonsterManager MonsterManager;

        public bool IsMonsterComboAvailableOnBoard( IGamePiece[,] i_board ) {
            List<List<int>> combosToCheck = MonsterManager.GetCurrentMonsterCombos();
            if ( combosToCheck == null || combosToCheck.Count == 0 ) {
                return true;    // this doesn't feel like clean code, but if the combosToCheck is null/0, we don't want to randomize
            }

            bool doesBoardHaveAnyCombo = false;
            
            foreach ( List<int> combo in combosToCheck ) {
                if ( IsComboOnBoard( combo, i_board ) ) {
                    doesBoardHaveAnyCombo = true;
                    break;
                }
            }

            return doesBoardHaveAnyCombo;
        }

        private bool IsComboOnBoard( List<int> i_combo, IGamePiece[,] i_board ) {
            for ( int i = 0; i < i_board.GetLength( 0 ); ++i ) {
                for ( int j = 0; j < i_board.GetLength( 1 ); ++j ) {
                    List<IGamePiece> test = new List<IGamePiece>();
                    if ( SearchBoardForCombo( i, j, test, i_combo, i_board ) ) {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AreRowOrColumnOutOfBounds( int i_row, int i_col, IGamePiece[,] i_board ) {
            if ( i_row < 0 || i_row >= i_board.GetLength( 0 ) || i_col < 0 || i_col >= i_board.GetLength( 1 ) ) {
                return true;
            }
            else {
                return false;
            }
        }

        private bool IsValidPieceForChain( IGamePiece i_piece, List<IGamePiece> i_chainInProgress, List<int> i_targetChain ) {
            if ( i_chainInProgress.Contains( i_piece ) || i_piece.PieceType != i_targetChain[i_chainInProgress.Count] ) {
                return false;
            }
            else {
                return true;
            }
        }

        private bool SearchBoardForCombo( int i_row, int i_col, List<IGamePiece> i_chainInProgress, List<int> i_targetChain, IGamePiece[,] i_board ) {
            if ( AreRowOrColumnOutOfBounds( i_row, i_col, i_board ) ) {
                return false;
            }

            IGamePiece onPiece = i_board[i_row, i_col];
            if ( IsValidPieceForChain( onPiece, i_chainInProgress, i_targetChain ) ) {
                i_chainInProgress.Add( onPiece );
            }
            else {
                return false;
            }

            if ( i_chainInProgress.Count == i_targetChain.Count ) {
                return true;    // entire combo was found!
            }

            // getting here means we have a match so far, but entire combo not yet found
            if ( SearchBoardForCombo( i_row + 1, i_col, i_chainInProgress, i_targetChain, i_board ) ||
                SearchBoardForCombo( i_row - 1, i_col, i_chainInProgress, i_targetChain, i_board ) ||
                SearchBoardForCombo( i_row, i_col + 1, i_chainInProgress, i_targetChain, i_board ) ||
                SearchBoardForCombo( i_row, i_col - 1, i_chainInProgress, i_targetChain, i_board ) ) {
                return true;
            }
            else {
                i_chainInProgress.Remove( onPiece );
                return false;
            }
        }
    }
}
