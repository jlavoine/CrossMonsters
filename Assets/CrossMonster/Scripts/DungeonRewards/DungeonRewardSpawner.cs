
namespace MonsterMatch {
    public interface IDungeonRewardSpawner {
        IDungeonReward Create( IDungeonRewardData i_data);
    }

    public class DungeonRewardSpawner : IDungeonRewardSpawner {
        readonly DungeonReward.Factory factory;

        public DungeonRewardSpawner( DungeonReward.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IDungeonReward Create( IDungeonRewardData i_data ) {
            return factory.Create( i_data );
        }
    }
}