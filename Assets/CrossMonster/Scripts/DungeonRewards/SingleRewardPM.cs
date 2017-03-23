using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class SingleRewardPM : PresentationModel, ISingleRewardPM {
        public const string COVER_VISIBLE_PROPERTY = "IsCoverVisible";
        public const string COUNT_PROPERTY = "Count";
        public const string NAME_PROPERTY = "Name";

        readonly IStringTableManager mStringTable;
        readonly IAllRewardsPM mAllRewardsPM;

        private IDungeonReward mReward;

        public SingleRewardPM( IStringTableManager i_stringTable, IDungeonReward i_reward, IAllRewardsPM i_allRewardsPM ) {
            mReward = i_reward;
            mStringTable = i_stringTable;
            mAllRewardsPM = i_allRewardsPM;

            SetCoverVisibleProperty( true );
            SetCountProperty();
            SetNameProperty();
        }

        public void UncoverReward() {
            SetCoverVisibleProperty( false );
            mAllRewardsPM.RewardUncovered();
        }

        private void SetCoverVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( COVER_VISIBLE_PROPERTY, i_visible );
        }

        private void SetCountProperty() {
            ViewModel.SetProperty( COUNT_PROPERTY, mReward.GetCount().ToString() );
        }

        private void SetNameProperty() {
            string text = mStringTable.Get( mReward.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        public class Factory : Factory<IDungeonReward, IAllRewardsPM, SingleRewardPM> { }
    }
}
