using MyLibrary;

namespace CrossMonsters {
    public class TreasureSetPM : PresentationModel, ITreasureSetPM {
        public const string NAME_PROPERTY = "TreasureSetName";

        private ITreasureSetData mData;

        public TreasureSetPM( ITreasureSetData i_data ) {
            mData = i_data;

            SetNameProperty();
        }

        private void SetNameProperty() {
            string text = StringTableManager.Instance.Get( mData.GetId() + "_Name" );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }
    }
}