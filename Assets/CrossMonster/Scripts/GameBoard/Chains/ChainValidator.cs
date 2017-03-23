using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainValidator : IChainValidator {
        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            return false;
        }
    }
}