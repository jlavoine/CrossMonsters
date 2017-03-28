using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class ChainValidator : IChainValidator {
        [Inject]
        IChainValidator_DuplicatePieces DuplicatePieceValidator;

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            return DuplicatePieceValidator.IsValidPieceInChain( i_piece, i_chain );
        }
    }
}