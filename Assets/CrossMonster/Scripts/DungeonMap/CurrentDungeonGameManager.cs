using System.Collections.Generic;

namespace MonsterMatch {
    public class CurrentDungeonGameManager : ICurrentDungeonGameManager {
        private const int MONSTER_WAVE_COUNT = 3; // temp
        readonly IMonsterDataManager mMonsterDataManager;
        readonly IDungeonRewardSpawner mRewardSpawner;

        private IDungeonGameSessionData mData;
        public IDungeonGameSessionData Data { get { return mData; } set { mData = value; } }

        private List<IMonsterWaveData> mMonsters;
        public List<IMonsterWaveData> Monsters { get { return mMonsters; } set { mMonsters = value; } }

        private List<IDungeonReward> mRewards;
        public List<IDungeonReward> Rewards { get { return mRewards; } set { mRewards = value; } }

        public CurrentDungeonGameManager( IMonsterDataManager i_monsterDataManager, IDungeonRewardSpawner i_rewardSpawner ) {
            mMonsterDataManager = i_monsterDataManager;
            mRewardSpawner = i_rewardSpawner;
        }

        public void SetData( IDungeonGameSessionData i_data ) {
            Data = i_data;
            SetMonsters();
            SetRewards();
        }

        public void AwardRewards() {
            foreach ( IDungeonReward reward in Rewards ) {
                reward.Award();
            }
        }

        private void SetMonsters() {
            Monsters = new List<IMonsterWaveData>();
            if ( Data.GetMonsters() != null ) {
                for ( int i = 0; i < MONSTER_WAVE_COUNT; ++i ) {
                    IMonsterWaveData wave = new MonsterWaveData();
                    foreach ( string monsterId in Data.GetMonsters() ) {
                        IMonsterData monsterData = mMonsterDataManager.GetData( monsterId );
                        IGameMonster monster = new GameMonster( monsterData );
                        wave.AddMonster( monster );
                    }
                    Monsters.Add( wave );
                }
            }
        }

        private void SetRewards() {
            Rewards = new List<IDungeonReward>();
            if ( Data.GetRewards() != null ) {
                foreach ( IDungeonRewardData data in Data.GetRewards() ) {
                    Rewards.Add( mRewardSpawner.Create( data ) );
                }                
            }
        }
    }
}
