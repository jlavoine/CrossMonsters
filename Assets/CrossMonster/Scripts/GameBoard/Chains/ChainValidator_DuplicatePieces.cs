using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainValidator_DuplicatePieces : IChainValidator_DuplicatePieces {
        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            return !i_chain.Contains( i_piece );
        }
    }
}
