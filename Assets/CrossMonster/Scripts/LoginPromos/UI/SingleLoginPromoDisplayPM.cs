using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class SingleLoginPromoDisplayPM : BasicWindowPM, ISingleLoginPromoDisplayPM {
        public const string TITLE_PROPERTY = "Title";

        readonly IStringTableManager mStringTable;
        readonly ILoginPromotionData mData;

        public SingleLoginPromoDisplayPM( IStringTableManager i_stringTable, ILoginPromotionData i_data ) {
            mStringTable = i_stringTable;
            mData = i_data;

            SetVisibleProperty( false );
            SetTitle();
        }

        public void UpdateVisibilityBasedOnCurrentlyDisplayedPromo( string i_id ) {
            bool isVis = i_id == mData.GetId();
            SetVisibleProperty( isVis );
        }

        public string GetPrefab() {
            return mData.GetPromoPrefab();
        }

        private void SetTitle() {
            string title = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( TITLE_PROPERTY, title );
        }

        public class Factory : Factory<ILoginPromotionData, SingleLoginPromoDisplayPM> { }
    }
}