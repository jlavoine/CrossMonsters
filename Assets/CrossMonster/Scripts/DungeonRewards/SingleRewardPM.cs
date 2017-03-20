using MyLibrary;

namespace CrossMonsters {
    public class SingleRewardPM : PresentationModel, ISingleRewardPM {
        public const string COVER_VISIBLE_PROPERTY = "IsCoverVisible";
        public const string COUNT_PROPERTY = "Count";

        private IDungeonRewardData mData;

        public SingleRewardPM( IDungeonRewardData i_data ) {
            mData = i_data;

            SetCoverVisibleProperty( true );
            SetCountProperty();
        }

        public void UncoverReward() {
            SetCoverVisibleProperty( false );
        }

        private void SetCoverVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( COVER_VISIBLE_PROPERTY, i_visible );
        }

        private void SetCountProperty() {
            ViewModel.SetProperty( COUNT_PROPERTY, mData.GetCount().ToString() );
        }
    }
}
