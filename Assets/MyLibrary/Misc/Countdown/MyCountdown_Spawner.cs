
namespace MyLibrary {
    public interface IMyCountdown_Spawner {
        IMyCountdown Create( long i_targetTimeMs, ICountdownCallback i_callback );
    }

    public class MyCountdown_Spawner : IMyCountdown_Spawner {
        readonly MyCountdown.Factory factory;

        public MyCountdown_Spawner( MyCountdown.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IMyCountdown Create( long i_targetTimeMs, ICountdownCallback i_callback ) {
            return factory.Create( i_targetTimeMs, i_callback );
        }
    }
}