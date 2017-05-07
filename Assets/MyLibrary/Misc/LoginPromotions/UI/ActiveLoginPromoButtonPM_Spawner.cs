
namespace MyLibrary {
    public interface IActiveLoginPromoButtonPM_Spawner {
        IActiveLoginPromoButtonPM Create( ILoginPromotionData i_data );
    }

    public class ActiveLoginPromoButtonPM_Spawner : IActiveLoginPromoButtonPM_Spawner {
        readonly ActiveLoginPromoButtonPM.Factory factory;

        public ActiveLoginPromoButtonPM_Spawner( ActiveLoginPromoButtonPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IActiveLoginPromoButtonPM Create( ILoginPromotionData i_data ) {
            return factory.Create( i_data );
        }
    }
}