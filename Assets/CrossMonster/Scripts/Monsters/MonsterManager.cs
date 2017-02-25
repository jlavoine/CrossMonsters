using System.Collections.Generic;

namespace CrossMonsters {
    public class MonsterManager {
        private List<IGameMonster> mMonsters;
        public List<IGameMonster> Monsters { get { return mMonsters; } set { mMonsters = value; } }

        public void Tick( long i_time ) {
            foreach ( IGameMonster monster in Monsters ) {
                monster.Tick( i_time );
            }
        }
    }
}
