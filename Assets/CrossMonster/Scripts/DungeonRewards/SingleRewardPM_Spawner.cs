
namespace CrossMonsters {
    public interface ISingleRewardPM_Spawner {
        ISingleRewardPM Create( IDungeonRewardData i_data, IAllRewardsPM i_pm );
    }

    public class SingleRewardPM_Spawner : ISingleRewardPM_Spawner {
        readonly SingleRewardPM.Factory factory;

        public SingleRewardPM_Spawner( SingleRewardPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ISingleRewardPM Create( IDungeonRewardData i_data, IAllRewardsPM i_pm ) {
            return factory.Create( i_data, i_pm );
        }
    }
}