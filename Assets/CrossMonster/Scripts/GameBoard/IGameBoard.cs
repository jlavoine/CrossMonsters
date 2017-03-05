
namespace CrossMonsters {
    public interface IGameBoard {
        IGamePiece[,] BoardPieces { get; }

        void RandomizeBoard();
    }
}
