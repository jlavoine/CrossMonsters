using MyLibrary;

namespace CrossMonsters {
    public interface IGamePiecePM : IPresentationModel {
        IGamePiece GamePiece { get; }
    }
}
