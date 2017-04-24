
namespace MonsterMatch {
    public class DungeonRewardData : IDungeonRewardData {       
        public string Id;
        public LootTypes LootType;
        public int Count;

        public string GetId() {
            return Id;
        }

        public LootTypes GetLootType() {
            return LootType;
        }

        public int GetCount() {
            return Count;
        }
    }
}
