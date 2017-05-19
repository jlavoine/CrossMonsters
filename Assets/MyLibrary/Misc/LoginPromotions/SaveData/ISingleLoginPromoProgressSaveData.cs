
namespace MyLibrary {
    public interface ISingleLoginPromoProgressSaveData {
        string GetId();
        long GetLastCollectedTime();
        int GetCollectCount();

        bool AreRewardsRemaining( ILoginPromotionManager i_manager );
        bool HasRewardBeenClaimedToday( IBackendManager i_backend );
    }
}