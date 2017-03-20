
namespace CrossMonsters {
    public class DungeonRewardData : IDungeonRewardData {
        public const string NAME_KEY = "_Name";

        public string Id;
        public LootTypes LootType;
        public int Count;

        public string GetId() {
            return Id;
        }

        public string GetNameKey() {
            return Id + NAME_KEY;
        }

        public LootTypes GetLootType() {
            return LootType;
        }

        public int GetCount() {
            return Count;
        }
    }
}
