using MyLibrary;

namespace MonsterMatch {
    public interface IDamageCalculator {
        int GetDamageFromMonster( IGameMonster i_monster, IGamePlayer i_player );
    }
}
