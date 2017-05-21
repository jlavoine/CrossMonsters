using MyLibrary;

namespace MonsterMatch {
    public class LoginPromoPopupHelper : ILoginPromoPopupHelper {
        readonly ILoginPromotionManager mPromoManager;
        readonly ILoginPromoDisplaysPM mAllPromosPM;
        readonly IBackendManager mBackend;

        public LoginPromoPopupHelper( IBackendManager i_backend, ILoginPromotionManager i_manager, ILoginPromoDisplaysPM i_allDisplaysPM ) {
            mPromoManager = i_manager;
            mAllPromosPM = i_allDisplaysPM;
            mBackend = i_backend;
        }

        public bool ShouldShowPromoAsPopup( ISingleLoginPromoProgressSaveData i_promoData ) {
            bool doesHaveUI = mAllPromosPM.DoesHaveDisplayForPromo( i_promoData.GetId() );
            bool areRewardsRemaining = i_promoData.AreRewardsRemaining( mPromoManager.GetDataForPromo( i_promoData.GetId() ) );
            bool hasRewardBeenClaimedToday = i_promoData.HasRewardBeenClaimedToday( mBackend );

            return doesHaveUI && areRewardsRemaining && !hasRewardBeenClaimedToday;
        }
    }
}
