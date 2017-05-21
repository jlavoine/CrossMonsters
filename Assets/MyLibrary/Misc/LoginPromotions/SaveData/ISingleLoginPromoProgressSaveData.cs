
namespace MyLibrary {
    public interface ISingleLoginPromoProgressSaveData {
        string GetId();
        long GetLastCollectedTime();
        int GetCollectCount();

        bool AreRewardsRemaining( ILoginPromotionData i_promoData );
        bool HasRewardBeenClaimedToday( IBackendManager i_backend );
    }
}