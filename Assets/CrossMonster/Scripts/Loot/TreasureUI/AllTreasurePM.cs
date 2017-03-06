using MyLibrary;

namespace CrossMonsters {
    public class AllTreasurePM : PresentationModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public AllTreasurePM() {
            SetVisibleProperty( false );
        }

        public void Show() {
            SetVisibleProperty( true );
        }

        public void Hide() {
            SetVisibleProperty( false );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
