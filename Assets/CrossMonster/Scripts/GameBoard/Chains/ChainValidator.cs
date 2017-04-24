﻿using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class ChainValidator : IChainValidator {
        [Inject]
        IChainValidator_DuplicatePieces DuplicatePieceValidator;

        [Inject]
        IChainValidator_DiagonalPieces DiagonalPieceValidator;

        [Inject]
        IChainValidator_StraightLinesOnly StraightLinesOnlyValidator;

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            return DuplicatePieceValidator.IsValidPieceInChain( i_piece, i_chain )
                && DiagonalPieceValidator.IsValidPieceInChain( i_piece, i_chain )
                && StraightLinesOnlyValidator.IsValidPieceInChain( i_piece, i_chain );
        }
    }
}