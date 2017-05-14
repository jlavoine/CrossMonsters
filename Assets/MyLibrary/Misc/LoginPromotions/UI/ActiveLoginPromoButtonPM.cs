using Zenject;

namespace MyLibrary {
    public class ActiveLoginPromoButtonPM : PresentationModel, IActiveLoginPromoButtonPM {
        public const string NAME_PROPERTY = "Name";
        public const string PROMO_VISIBLE_PROPERTY = "IsPromoVisible";

        readonly ILoginPromoDisplaysPM mPromoDisplayPM;
        readonly IStringTableManager mStringTable;
        readonly ILoginPromotionData mData;

        public ActiveLoginPromoButtonPM( ILoginPromoDisplaysPM i_promoDisplayPM, IStringTableManager i_stringTable, ILoginPromotionData i_data ) {
            mPromoDisplayPM = i_promoDisplayPM;
            mStringTable = i_stringTable;
            mData = i_data;

            SetNameProperty();
            SetPromoVisibility( false );
        }

        public void OpenDisplayClicked() {
            mPromoDisplayPM.DisplayPromoAndHideOthers( mData.GetId() );
        }

        private void SetNameProperty() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        private void SetPromoVisibility( bool i_visible ) {
            ViewModel.SetProperty( PROMO_VISIBLE_PROPERTY, i_visible );
        }

        public class Factory : Factory<ILoginPromotionData, ActiveLoginPromoButtonPM> { }
    }
}
