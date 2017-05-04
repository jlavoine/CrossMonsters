
namespace MonsterMatch {
    public interface IOpenTimedChestResponse {
        IDungeonRewardData GetReward();
        long GetNextAvailableTime();
        bool IsOpeningVerified();
    }

    public class OpenTimedChestResponse : IOpenTimedChestResponse {
        public DungeonRewardData Reward;
        public long NextAvailableTime;

        public IDungeonRewardData GetReward() {
            return Reward;
        }

        public long GetNextAvailableTime() {
            return NextAvailableTime;
        }

        public bool IsOpeningVerified() {
            return Reward != null;
        }
    }
}
