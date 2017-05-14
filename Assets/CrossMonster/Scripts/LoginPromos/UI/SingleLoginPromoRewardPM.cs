using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class SingleLoginPromoRewardPM : PresentationModel, ISingleLoginPromoRewardPM {
        public const string REWARD_NUMBER_PROPERTY = "RewardNumber";

        public SingleLoginPromoRewardPM( int i_dayNumber, IGameRewardData i_rewardData ) {
            SetRewardNumberProperty( i_dayNumber );
        }

        private void SetRewardNumberProperty( int i_dayNumber ) {
            ViewModel.SetProperty( REWARD_NUMBER_PROPERTY, i_dayNumber.ToString() );
        }

        public class Factory : Factory<int, IGameRewardData, SingleLoginPromoRewardPM> { }
    }
}
