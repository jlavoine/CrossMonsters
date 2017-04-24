using System.Collections.Generic;

namespace MonsterMatch {
    public interface IChainValidator {
        bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain );
    }
}
