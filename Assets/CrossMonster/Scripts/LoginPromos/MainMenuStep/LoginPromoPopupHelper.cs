using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class LoginPromoPopupHelper : ILoginPromoPopupHelper {
        public const string PROMO_ID = "Id";

        readonly ILoginPromoDisplaysPM mAllPromosPM;
        readonly IBackendManager mBackend;
        readonly IDungeonRewardSpawner mRewardSpawner;

        public LoginPromoPopupHelper( IDungeonRewardSpawner i_rewardSpawner, IBackendManager i_backend, ILoginPromoDisplaysPM i_allDisplaysPM ) {
            mAllPromosPM = i_allDisplaysPM;
            mBackend = i_backend;
            mRewardSpawner = i_rewardSpawner;
        }

        public bool ShouldShowPromoAsPopup( ISingleLoginPromoProgressSaveData i_promoProgress, ILoginPromotionData i_promoData ) {
            bool doesHaveUI = mAllPromosPM.DoesHaveDisplayForPromo( i_promoData.GetId() );
            bool areRewardsRemaining = i_promoProgress.AreRewardsRemaining( i_promoData );
            bool hasRewardBeenClaimedToday = i_promoProgress.HasRewardBeenClaimedToday( mBackend );

            return doesHaveUI && areRewardsRemaining && !hasRewardBeenClaimedToday;
        }

        public void AwardPromoOnClient( ISingleLoginPromoProgressSaveData i_promoProgress, ILoginPromotionData i_promoData ) {
            i_promoProgress.OnAwarded( mBackend.GetTimeInMs() );

            IGameRewardData rewardData = i_promoData.GetRewardDataForDay( i_promoProgress.GetCollectCount() );
            IDungeonReward reward = mRewardSpawner.Create( rewardData );
            if ( reward != null ) {
                reward.Award();
            }
        }

        public void AwardPromoOnServer( ILoginPromotionData i_promoData ) {
            Dictionary<string, string> cloudParams = new Dictionary<string, string>();
            cloudParams.Add( PROMO_ID, i_promoData.GetId() );

            mBackend.MakeCloudCall( BackendMethods.UPDATE_LOGIN_PROMO_PROGRESS, cloudParams, ( result ) => { } );
        }
    }
}
