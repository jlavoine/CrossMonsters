using MyLibrary;

namespace CrossMonsters {
    public interface IGamePiece : IBusinessModel {
        int PieceType { get; }

        void UsePiece();
    }
}
