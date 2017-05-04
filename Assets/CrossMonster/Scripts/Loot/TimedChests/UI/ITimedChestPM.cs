using MyLibrary;

namespace MonsterMatch {
    public interface ITimedChestPM : IPresentationModel {
        ISingleRewardPM RewardPM { get; }

        void Open();
        void UpdateProperties();
        void ShowOpenReward( IDungeonReward i_reward );
    }
}
