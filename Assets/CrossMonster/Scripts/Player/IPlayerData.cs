
namespace CrossMonsters {
    public interface IPlayerData {
        int GetHP();
        int GetDefenseForType( int i_type );
        int GetAttackForType( int i_type );
    }
}