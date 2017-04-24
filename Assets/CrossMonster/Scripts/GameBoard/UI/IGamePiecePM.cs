using MyLibrary;

namespace MonsterMatch {
    public interface IGamePiecePM : IPresentationModel {
        IGamePiece GamePiece { get; }
    }
}
