using Zenject;

namespace MyLibrary {
    public class ActiveLoginPromoButtonPM : PresentationModel, IActiveLoginPromoButtonPM {

        public ActiveLoginPromoButtonPM( ILoginPromotionData i_data ) {

        }

        public class Factory : Factory<ILoginPromotionData, ActiveLoginPromoButtonPM> { }
    }
}
