using System.Collections.Generic;

namespace CrossMonsters {
    public class MonsterManager {
        private List<IGameMonster> mCurrentMonsters;
        public List<IGameMonster> CurrentMonsters { get { return mCurrentMonsters; } set { mCurrentMonsters = value; } }

        private List<IGameMonster> mRemainingMonsters;
        public List<IGameMonster> RemainingMonsters { get { return mRemainingMonsters; } set { mRemainingMonsters = value; } }

        public void Tick( long i_time ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                monster.Tick( i_time );
            }
        }
    }
}
