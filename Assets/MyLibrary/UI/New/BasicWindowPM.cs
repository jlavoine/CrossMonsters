
namespace MyLibrary {
    public abstract class BasicWindowPM : PresentationModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public void Show() {
            SetVisibleProperty( true );
        }

        public void Hide() {
            SetVisibleProperty( false );
        }

        protected void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
