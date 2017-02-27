using MyLibrary;

namespace CrossMonsters {
    public interface IDamageCalculator {
        int GetDamageFromMonster( IGameMonster i_monster, IGamePlayer i_player );
    }
}
