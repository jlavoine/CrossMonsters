using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class SingleLoginPromoRewardPM : PresentationModel, ISingleLoginPromoRewardPM {
        public const string REWARD_NUMBER_PROPERTY = "RewardNumber";

        readonly IDungeonRewardSpawner mRewardSpawner;
        readonly ISingleRewardPM_Spawner mRewardPMSpawner;

        private ISingleRewardPM mRewardPM;
        public ISingleRewardPM RewardPM { get { return mRewardPM; } set { mRewardPM = value; } }

        public SingleLoginPromoRewardPM( IDungeonRewardSpawner i_rewardSpawner, ISingleRewardPM_Spawner i_rewardPMSpawner, int i_dayNumber, IGameRewardData i_rewardData ) {
            mRewardSpawner = i_rewardSpawner;
            mRewardPMSpawner = i_rewardPMSpawner;

            CreateRewardPM( i_rewardData );
            SetRewardNumberProperty( i_dayNumber );
        }

        private void CreateRewardPM( IGameRewardData i_rewardData ) {
            IDungeonReward reward = mRewardSpawner.Create( i_rewardData );
            RewardPM = mRewardPMSpawner.Create( reward, null );
            RewardPM.UncoverReward();
        }

        private void SetRewardNumberProperty( int i_dayNumber ) {
            ViewModel.SetProperty( REWARD_NUMBER_PROPERTY, i_dayNumber.ToString() );
        }

        public class Factory : Factory<int, IGameRewardData, SingleLoginPromoRewardPM> { }
    }
}
