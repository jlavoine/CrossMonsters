using System.Collections.Generic;

namespace MonsterMatch {
    public interface IGameRules {
        int GetGamePieceRotation( int i_pieceType );
        int GetActiveMonsterCount();
        int GetBoardSize();
        List<int> GetPieceTypes();
    }
}
