
namespace MyLibrary {
    public abstract class BasicWindowPM : PresentationModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public void Show() {
            SetVisibleProperty( true );
            OnShown();
        }

        public void Hide() {
            SetVisibleProperty( false );
            OnHidden();
        }

        protected void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }

        // virtual not abstract as to not force child classes to implement
        protected virtual void OnShown() { }
        protected virtual void OnHidden() { }
    }
}
