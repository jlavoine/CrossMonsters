
namespace MonsterMatch {
    public interface IOpenTimedChestResponse {
        IDungeonRewardData GetReward();
    }

    public class OpenTimedChestResponse : IOpenTimedChestResponse {
        public DungeonRewardData Reward;

        public IDungeonRewardData GetReward() {
            return Reward;
        }
    }
}
