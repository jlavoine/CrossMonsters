using MyLibrary;

namespace MonsterMatch {
    public interface IGamePiece : IBusinessModel {
        int PieceType { get; }
        int Index { get; }

        void UsePiece();
        void Randomize();
    }
}
