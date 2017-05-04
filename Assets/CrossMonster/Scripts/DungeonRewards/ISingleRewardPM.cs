using MyLibrary;

namespace MonsterMatch {
    public interface ISingleRewardPM : IPresentationModel {
        void UncoverReward();
        void SetReward( IDungeonReward i_reward );
    }
}