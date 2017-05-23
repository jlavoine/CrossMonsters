using MyLibrary;

namespace MonsterMatch {
    public interface ILoginPromoPopupHelper {
        bool ShouldShowPromoAsPopup( ISingleLoginPromoProgressSaveData i_promoProgress, ILoginPromotionData i_promoData );
        void AwardPromoOnClient( ISingleLoginPromoProgressSaveData i_promoProgress, ILoginPromotionData i_promoData );
        void AwardPromoOnServer( ILoginPromotionData i_promoData );
    }
}