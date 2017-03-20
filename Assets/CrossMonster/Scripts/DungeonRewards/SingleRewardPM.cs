using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class SingleRewardPM : PresentationModel, ISingleRewardPM {
        public const string COVER_VISIBLE_PROPERTY = "IsCoverVisible";
        public const string COUNT_PROPERTY = "Count";
        public const string NAME_PROPERTY = "Name";

        readonly IStringTableManager mStringTable;

        private IDungeonRewardData mData;

        public SingleRewardPM( IStringTableManager i_stringTable, IDungeonRewardData i_data ) {
            mData = i_data;
            mStringTable = i_stringTable;

            SetCoverVisibleProperty( true );
            SetCountProperty();
            SetNameProperty();
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

        private void SetNameProperty() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        public class Factory : Factory<IDungeonRewardData, SingleRewardPM> { }
    }
}
