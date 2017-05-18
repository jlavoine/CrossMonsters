
namespace MyLibrary {
    public class SingleLoginPromoProgressSaveData : ISingleLoginPromoProgressSaveData {
        public string Id;
        public long LastCollectedTime;
        public int CollectCount;

        public string GetId() {
            return Id;
        }

        public long GetLastCollectedTime() {
            return LastCollectedTime;
        }

        public int GetCollectCount() {
            return CollectCount;
        }
    }
}
