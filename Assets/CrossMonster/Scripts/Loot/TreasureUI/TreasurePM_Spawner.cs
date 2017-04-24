
namespace MonsterMatch {
    public interface ITreasurePM_Spawner {
        ITreasurePM Create( string i_id );
    }

    public class TreasurePM_Spawner : ITreasurePM_Spawner {
        readonly TreasurePM.Factory factory;

        public TreasurePM_Spawner( TreasurePM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ITreasurePM Create( string i_id ) {
            return factory.Create( i_id );
        }
    }
}