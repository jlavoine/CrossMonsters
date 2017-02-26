
namespace CrossMonsters {
    public interface IGamePlayer {
        int GetAttackPowerForType( int i_type );
        int HP { get; }
    }
}
