using System.Collections.Generic;

namespace MyLibrary {
    public interface ILoginPromotionManager {
        void Init( IBasicBackend i_backend );

        List<ILoginPromotionData> ActivePromotionData { get; set; }
        Dictionary<string, ISingleLoginPromoProgressSaveData> PromoProgress { get; set; }

        List<ISingleLoginPromoProgressSaveData> GetActivePromoSaveData();
    }
}
