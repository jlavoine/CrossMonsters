using MyLibrary;

namespace MonsterMatch {
    public interface IOpenTimedChestResponse {
        IGameRewardData GetReward();
        long GetNextAvailableTime();
        bool IsOpeningVerified();
    }

    public class OpenTimedChestResponse : IOpenTimedChestResponse {
        public GameRewardData Reward;
        public long NextAvailableTime;

        public IGameRewardData GetReward() {
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
