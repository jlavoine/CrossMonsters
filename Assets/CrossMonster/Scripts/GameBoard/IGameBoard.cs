using System.Collections.Generic;

namespace MonsterMatch {
    public interface IGameBoard {
        IGamePiece[,] BoardPieces { get; }

        void RandomizeGameBoardIfNoMonsterCombosAvailable();
    }
}
