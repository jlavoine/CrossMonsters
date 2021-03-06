﻿using MyLibrary;

namespace MonsterMatch {
    public interface IGamePiece : IBusinessModel {
        GamePieceStates State { get; set; }
        int PieceType { get; }
        int Index { get; }

        bool IsSelectable();

        void UsePiece();
        void Randomize();
        void PieceFailedMatch();
    }
}
