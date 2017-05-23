
namespace MyLibrary {
    public interface ISingleLoginPromoProgressSaveData {
        string GetId();
        long GetLastCollectedTime();
        int GetCollectCount();

        void OnAwarded( long i_curTime );

        bool AreRewardsRemaining( ILoginPromotionData i_promoData );
        bool HasRewardBeenClaimedToday( IBackendManager i_backend );
    }
}