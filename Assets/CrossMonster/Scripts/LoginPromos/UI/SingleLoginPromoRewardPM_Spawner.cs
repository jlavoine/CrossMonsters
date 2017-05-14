using MyLibrary;

namespace MonsterMatch {
    public interface ISingleLoginPromoRewardPM_Spawner {
        ISingleLoginPromoRewardPM Create( int i_dayNumber, IGameRewardData i_reward );
    }

    public class SingleLoginPromoRewardPM_Spawner : ISingleLoginPromoRewardPM_Spawner {
        readonly SingleLoginPromoRewardPM.Factory factory;

        public SingleLoginPromoRewardPM_Spawner( SingleLoginPromoRewardPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ISingleLoginPromoRewardPM Create( int i_dayNumber, IGameRewardData i_reward ) {
            return factory.Create( i_dayNumber, i_reward );
        }
    }
}