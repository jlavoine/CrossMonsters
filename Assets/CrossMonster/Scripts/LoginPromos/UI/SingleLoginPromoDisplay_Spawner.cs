using MyLibrary;

namespace MonsterMatch {
    public interface ISingleLoginPromoPM_Spawner {
        ISingleLoginPromoDisplayPM Create( ILoginPromotionData i_data );
    }

    public class SingleLoginPromoPM_Spawner : ISingleLoginPromoPM_Spawner {
        readonly SingleLoginPromoDisplayPM.Factory factory;

        public SingleLoginPromoPM_Spawner( SingleLoginPromoDisplayPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ISingleLoginPromoDisplayPM Create( ILoginPromotionData i_data ) {
            return factory.Create( i_data );
        }
    }
}