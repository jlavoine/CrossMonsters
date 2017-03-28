using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class ChainValidator_StraightLinesOnly : IChainValidator_StraightLinesOnly {

        [Inject]
        ICurrentDungeonGameManager CurrentDungeon;

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            if ( i_chain.Count == 0 || i_chain.Count == 1 || !CurrentDungeon.Data.StraightMovesOnly() ) {
                return true;
            }

            int direction = GetDirectionOfChain( i_chain );
            bool isStraightLine = IsStraightLine( i_piece.Index, i_chain[i_chain.Count - 1].Index, direction );

            return isStraightLine;
        }

        private int GetDirectionOfChain( List<IGamePiece> i_chain ) {
            int firstPieceIndex = i_chain[0].Index;
            int secondPieceIndex = i_chain[1].Index;
            int direction = secondPieceIndex - firstPieceIndex;

            return direction;
        }

        private bool IsStraightLine( int i_incomingPieceIndex, int i_lastPlayedIndex, int i_direction ) {
            return i_lastPlayedIndex + i_direction == i_incomingPieceIndex;
        }
    }
}
