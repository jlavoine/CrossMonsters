
namespace MonsterMatch {
    public interface ITreasureSpawner {
        ITreasure Create( ITreasureData i_data );
    }

    public class TreasureSpawner : ITreasureSpawner {
        readonly Treasure.Factory factory;

        public TreasureSpawner( Treasure.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ITreasure Create( ITreasureData i_data ) {
            return factory.Create( i_data );
        }
    }
}