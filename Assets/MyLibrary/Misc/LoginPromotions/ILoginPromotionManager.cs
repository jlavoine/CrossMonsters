using System.Collections.Generic;

namespace MyLibrary {
    public interface ILoginPromotionManager {
        void Init( IBasicBackend i_backend );

        Dictionary<string, ILoginPromotionData> ActivePromotionData { get; set; }
        Dictionary<string, ISingleLoginPromoProgressSaveData> PromoProgress { get; set; }

        ILoginPromotionData GetDataForPromo( string i_id );

        List<ISingleLoginPromoProgressSaveData> GetActivePromoSaveData();
    }
}
