
namespace MonsterMatch {
    public interface ITreasureSetPM_Spawner {
        ITreasureSetPM Create( ITreasureSetData i_data );
    }

    public class TreasureSetPM_Spawner : ITreasureSetPM_Spawner {
        readonly TreasureSetPM.Factory factory;

        public TreasureSetPM_Spawner( TreasureSetPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ITreasureSetPM Create( ITreasureSetData i_data ) {
            return factory.Create( i_data );
        }
    }
}
