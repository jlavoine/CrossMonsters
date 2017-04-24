
namespace MonsterMatch {
    public interface IValidBoardChecker {
        bool IsMonsterComboAvailableOnBoard( IGamePiece[,] i_board );
    }
}