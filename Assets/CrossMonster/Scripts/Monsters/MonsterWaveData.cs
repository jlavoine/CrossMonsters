using System.Collections.Generic;

namespace CrossMonsters {
    public class MonsterWaveData : IMonsterWaveData {
        private List<IGameMonster> mMonsters = new List<IGameMonster>();
        public List<IGameMonster> Monsters { get { return mMonsters; } set { mMonsters = value; } }

        public void AddMonster( IGameMonster i_monster ) {
            mMonsters.Add( i_monster );
        }
    }
}