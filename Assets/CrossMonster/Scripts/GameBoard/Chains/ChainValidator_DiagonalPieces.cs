using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class ChainValidator_DiagonalPieces : IChainValidator_DiagonalPieces {
        [Inject]
        IGameRules GameRules;

        [Inject]
        ICurrentDungeonGameManager CurrentDungeon;

        private int mBoardSize;

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            if ( i_chain.Count == 0 || CurrentDungeon.Data.AllowDiagonals() ) {
                return true;
            }

            SetBoardSize();

            IGamePiece lastPlayedPiece = i_chain[i_chain.Count - 1];
            if ( IsLeftEdgePiece( lastPlayedPiece ) ) {
                return !IsIncomingPieceDiagonalFromLastPiece_ForLeftEdgePiece( i_piece.Index, lastPlayedPiece.Index );
            } else if ( IsRightEdgePiece( lastPlayedPiece ) ) {
                return !IsIncomingPieceDiagonalFromLastPiece_ForRightEdgePiece( i_piece.Index, lastPlayedPiece.Index );
            } else {
                return !IsIncomingPieceDiagonalFromLastPiece_ForMiddlePiece( i_piece.Index, lastPlayedPiece.Index );
            }
        }

        private void SetBoardSize() {
            mBoardSize = GameRules.GetBoardSize();
        }

        private bool IsLeftEdgePiece( IGamePiece i_piece ) {
            return i_piece.Index % GameRules.GetBoardSize() == 0;
        }

        private bool IsRightEdgePiece( IGamePiece i_piece ) {
            return i_piece.Index + 1 % GameRules.GetBoardSize() == 0;
        }

        private bool IsIncomingPieceDiagonalFromLastPiece_ForLeftEdgePiece( int i_incomingPieceIndex, int i_lastPlayedPieceIndex ) {
            int check_1 = i_lastPlayedPieceIndex + mBoardSize + 1;
            int check_2 = i_lastPlayedPieceIndex - mBoardSize + 1;

            return check_1 == i_incomingPieceIndex || check_2 == i_incomingPieceIndex;
        }

        private bool IsIncomingPieceDiagonalFromLastPiece_ForRightEdgePiece( int i_incomingPieceIndex, int i_lastPlayedPieceIndex ) {
            int check_1 = i_lastPlayedPieceIndex + mBoardSize - 1;
            int check_2 = i_lastPlayedPieceIndex - mBoardSize - 1;

            return check_1 == i_incomingPieceIndex || check_2 == i_incomingPieceIndex;
        }

        private bool IsIncomingPieceDiagonalFromLastPiece_ForMiddlePiece( int i_incomingPieceIndex, int i_lastPlayedPieceIndex ) {
            int check_1 = i_lastPlayedPieceIndex + mBoardSize + 1;
            int check_2 = i_lastPlayedPieceIndex - mBoardSize + 1;
            int check_3 = i_lastPlayedPieceIndex + mBoardSize - 1;
            int check_4 = i_lastPlayedPieceIndex - mBoardSize - 1;

            return check_1 == i_incomingPieceIndex || check_2 == i_incomingPieceIndex || check_3 == i_incomingPieceIndex || check_4 == i_incomingPieceIndex;
        }
    }
}
