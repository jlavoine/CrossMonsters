using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class DungeonReward : IDungeonReward {
        public const string NAME_KEY = "_Name";

        readonly IPlayerDataManager mPlayerData;

        private IGameRewardData mData;

        public DungeonReward( IPlayerDataManager i_playerData, IGameRewardData i_data ) {
            mPlayerData = i_playerData;
            mData = i_data;
        }

        public void Award() {
            switch ( GetLootType() ) {
                case LootTypes.Gold:
                    mPlayerData.Gold += GetCount();
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
            switch ( mData.GetLootType() ) {
                case "Gold":
                    return LootTypes.Gold;
                case "Treasure":
                    return LootTypes.Treasure;
                default:
                    return LootTypes.Gold;                                              
            }
        }

        public int GetCount() {
            return mData.GetCount();
        }

        public class Factory : Factory<IGameRewardData, DungeonReward> { }
    }
}
