using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterWaveData {
        void AddMonster( IGameMonster i_monster );
        List<IGameMonster> Monsters { get; set; }
    }
}