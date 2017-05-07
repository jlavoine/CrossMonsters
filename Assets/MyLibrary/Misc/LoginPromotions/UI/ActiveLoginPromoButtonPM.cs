using Zenject;

namespace MyLibrary {
    public class ActiveLoginPromoButtonPM : PresentationModel, IActiveLoginPromoButtonPM {
        public const string NAME_PROPERTY = "Name";

        readonly IStringTableManager mStringTable;
        readonly ILoginPromotionData mData;

        public ActiveLoginPromoButtonPM( IStringTableManager i_stringTable, ILoginPromotionData i_data ) {
            mStringTable = i_stringTable;
            mData = i_data;

            SetNameProperty();
        }

        private void SetNameProperty() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        public class Factory : Factory<ILoginPromotionData, ActiveLoginPromoButtonPM> { }
    }
}
