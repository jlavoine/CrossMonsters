using MyLibrary;

namespace MyLibrary {
    public interface ISingleRewardPM : IPresentationModel {
        void UncoverReward();
        void SetReward( IGameReward i_reward );
    }
}