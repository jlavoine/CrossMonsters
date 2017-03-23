using System.Collections.Generic;

namespace CrossMonsters {
    public interface IChainValidator {
        bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain );
    }
}
