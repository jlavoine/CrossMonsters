using MyLibrary;

namespace CrossMonsters {
    public interface IGamePiece : IBusinessModel {
        int PieceType { get; }
        int Index { get; }

        void UsePiece();
        void Randomize();
    }
}
