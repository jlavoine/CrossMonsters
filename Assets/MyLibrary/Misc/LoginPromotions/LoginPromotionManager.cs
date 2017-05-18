using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyLibrary {
    public class LoginPromotionManager : ILoginPromotionManager {
        public const string PROMO_PROGRESS_KEY = "LoginPromoProgress";
        public const string PROMOTIONS_TITLE_KEY = "LoginPromotions";

        private IBasicBackend mBackend;

        private List<ILoginPromotionData> mActivePromotionData = new List<ILoginPromotionData>();
        public List<ILoginPromotionData> ActivePromotionData { get { return mActivePromotionData; } set { mActivePromotionData = value; } }

        private Dictionary<string, ISingleLoginPromoProgressSaveData> mPromoProgress = new Dictionary<string, ISingleLoginPromoProgressSaveData>();
        public Dictionary<string, ISingleLoginPromoProgressSaveData> PromoProgress { get { return mPromoProgress; } set { mPromoProgress = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadAllPromotions();
            DownloadPromoProgress();
        }

        public List<ISingleLoginPromoProgressSaveData> GetActivePromoSaveData() {
            List<ISingleLoginPromoProgressSaveData> saveData = new List<ISingleLoginPromoProgressSaveData>();
            foreach ( KeyValuePair<string, ISingleLoginPromoProgressSaveData> kvp in PromoProgress ) {
                if ( IsPromoActive( kvp.Key ) ) {
                    saveData.Add( kvp.Value );
                }            
            }

            return saveData;
        }

        private bool IsPromoActive( string i_id ) {
            foreach ( ILoginPromotionData promoData in ActivePromotionData ) {
                if ( promoData.GetId() == i_id ) {
                    return true;
                }
            }

            return false;
        }

        private void DownloadAllPromotions() {
            mBackend.GetTitleData( PROMOTIONS_TITLE_KEY, ( result ) => {
                List<LoginPromotionData> allPromotionData = JsonConvert.DeserializeObject<List<LoginPromotionData>>( result );
                foreach ( ILoginPromotionData promoData in allPromotionData ) {
                    AddToActivePromosIfActive( promoData );
                }
            } );
        }

        private void DownloadPromoProgress() {
            mBackend.GetReadOnlyPlayerData( PROMO_PROGRESS_KEY, ( result ) => {
                Dictionary<string, SingleLoginPromoProgressSaveData>  progress = JsonConvert.DeserializeObject<Dictionary<string, SingleLoginPromoProgressSaveData>>( result );
                foreach (KeyValuePair<string, SingleLoginPromoProgressSaveData> kvp in progress ) {
                    PromoProgress.Add( kvp.Key, kvp.Value );
                }
            } );
        }

        public void AddToActivePromosIfActive( ILoginPromotionData i_promo ) {
            bool isActive = i_promo.IsActive( mBackend.GetDateTime() );
            if ( isActive ) {
                ActivePromotionData.Add( i_promo );
            }
        }
    }
}