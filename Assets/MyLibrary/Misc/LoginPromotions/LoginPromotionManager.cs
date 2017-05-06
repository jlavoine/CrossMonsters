using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyLibrary {
    public class LoginPromotionManager : ILoginPromotionManager {
        public const string PROMOTIONS_TITLE_KEY = "LoginPromotions";

        private IBasicBackend mBackend;

        private List<ILoginPromotionData> mActivePromotionData = new List<ILoginPromotionData>();
        public List<ILoginPromotionData> ActivePromotionData { get { return mActivePromotionData; } set { mActivePromotionData = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadAllPromotions();
        }

        private void DownloadAllPromotions() {
            mBackend.GetTitleData( PROMOTIONS_TITLE_KEY, ( result ) => {
                List<LoginPromotionData> allPromotionData = JsonConvert.DeserializeObject<List<LoginPromotionData>>( result );
                foreach ( ILoginPromotionData promoData in allPromotionData ) {
                    AddToActivePromosIfActive( promoData );
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