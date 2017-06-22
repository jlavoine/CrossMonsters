using System.Collections.Generic;

namespace MonsterMatch {
    public class ChainValidator_SelectablePieces : IChainValidator_SelectablePieces {

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            return i_piece.IsSelectable();
        }
    }
}