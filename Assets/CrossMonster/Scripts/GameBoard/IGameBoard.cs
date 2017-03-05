using System.Collections.Generic;

namespace CrossMonsters {
    public interface IGameBoard {
        IGamePiece[,] BoardPieces { get; }

        void RandomizeGameBoardIfNoMonsterCombosAvailable();
    }
}
