
namespace MonsterMatch {
    public interface ITimedChestPM_Spawner {
        ITimedChestPM Create( ITimedChestData i_data );
    }

    public class TimedChestPM_Spawner : ITimedChestPM_Spawner {
        readonly TimedChestPM.Factory factory;

        public TimedChestPM_Spawner( TimedChestPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ITimedChestPM Create( ITimedChestData i_data ) {
            return factory.Create( i_data );
        }
    }
}