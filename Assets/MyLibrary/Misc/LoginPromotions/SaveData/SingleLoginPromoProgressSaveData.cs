
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

        public bool AreRewardsRemaining( ILoginPromotionManager i_manager ) {
            return true;
        }

        public bool HasRewardBeenClaimedToday( IBackendManager i_backend ) {
            return false;
        }
    }
}
