
namespace MyLibrary {
    public class GameRewardData : IGameRewardData {
        public string Id;
        public string LootType;
        public int Count;

        public string GetId() {
            return Id;
        }

        public string GetLootType() {
            return LootType;
        }

        public int GetCount() {
            return Count;
        }
    }
}
