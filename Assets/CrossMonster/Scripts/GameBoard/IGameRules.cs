using System.Collections.Generic;

namespace CrossMonsters {
    public interface IGameRules {
        int GetGamePieceRotation( int i_pieceType );
        int GetActiveMonsterCount();
        int GetBoardSize();
        List<int> GetPieceTypes();
    }
}
