using System.Collections.Generic;

namespace CrossMonsters {
    public class CurrentDungeonGameManager : ICurrentDungeonGameManager {
        readonly IMonsterDataManager mMonsterDataManager;
        readonly IDungeonRewardSpawner mRewardSpawner;

        private IDungeonGameSessionData mData;
        public IDungeonGameSessionData Data { get { return mData; } set { mData = value; } }

        private List<IGameMonster> mMonsters;
        public List<IGameMonster> Monsters { get { return mMonsters; } set { mMonsters = value; } }

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
            Monsters = new List<IGameMonster>();
            if ( Data.GetMonsters() != null ) {
                foreach ( string monsterId in Data.GetMonsters() ) {
                    IMonsterData monsterData = mMonsterDataManager.GetData( monsterId );
                    IGameMonster monster = new GameMonster( monsterData );
                    Monsters.Add( monster );
                }
            }
        }

        private void SetRewards() {
            Rewards = new List<IDungeonReward>();
            if ( Data.GetRewards() != null ) {
                /*foreach ( DungeonRewardData data in Data.GetRewards() ) {
                    Rewards.Add( mRewardSpawner.Create( data ) );
                }*/
                
            }

            Rewards.Add( mRewardSpawner.Create( new DungeonRewardData() { Id = "Gold", Count = 100, LootType = LootTypes.Gold } ) );
            Rewards.Add( mRewardSpawner.Create( new DungeonRewardData() { Id = "Gold", Count = 50, LootType = LootTypes.Gold } ) );
        }
    }
}
