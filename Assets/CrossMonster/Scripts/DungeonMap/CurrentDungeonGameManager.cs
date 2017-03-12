using System.Collections.Generic;

namespace CrossMonsters {
    public class CurrentDungeonGameManager : ICurrentDungeonGameManager {
        readonly IMonsterDataManager mMonsterDataManager;

        private IDungeonGameSessionData mData;
        public IDungeonGameSessionData Data { get { return mData; } set { mData = value; } }

        private List<IGameMonster> mMonsters;
        public List<IGameMonster> Monsters { get { return mMonsters; } set { mMonsters = value; } }

        public CurrentDungeonGameManager( IMonsterDataManager i_monsterDataManager ) {
            mMonsterDataManager = i_monsterDataManager;
        }

        public void SetData( IDungeonGameSessionData i_data ) {
            Data = i_data;
            SetMonsters();
        }

        private void SetMonsters() {
            Monsters = new List<IGameMonster>();
            foreach ( string monsterId in Data.GetMonsters() ) {
                IMonsterData monsterData = mMonsterDataManager.GetData( monsterId );
                IGameMonster monster = new GameMonster( monsterData );
                Monsters.Add( monster );
            }
        }
    }
}
