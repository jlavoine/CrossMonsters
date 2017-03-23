using Zenject;

namespace CrossMonsters {
    public class DungeonReward : IDungeonReward {
        public const string NAME_KEY = "_Name";

        readonly IPlayerDataManager mPlayerData;

        private IDungeonRewardData mData;

        public DungeonReward( IPlayerDataManager i_playerData, IDungeonRewardData i_data ) {
            mPlayerData = i_playerData;
            mData = i_data;
        }

        public void Award() {
            switch ( GetLootType() ) {
                case LootTypes.Gold:
                    mPlayerData.Gold += GetCount();
                    UnityEngine.Debug.LogError( "Awarding " + GetCount() + " gold " );
                    break;
            }
        }

        public string GetId() {
            return mData.GetId();
        }

        public string GetNameKey() {
            return GetId() + NAME_KEY;
        }

        public LootTypes GetLootType() {
            return mData.GetLootType();
        }

        public int GetCount() {
            return mData.GetCount();
        }

        public class Factory : Factory<IDungeonRewardData, DungeonReward> { }
    }
}
