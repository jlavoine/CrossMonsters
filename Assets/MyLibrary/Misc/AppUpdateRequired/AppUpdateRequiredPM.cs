
namespace MyLibrary {
    public class AppUpdateRequiredPM : PresentationModel, IAppUpdateRequiredPM {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string BODY_TEXT_PROPERTY = "Body";
        public const string TITLE_TEXT_PROPERTY = "Title";

        public const string TITLE_KEY = "AppUpdateRequired_Title";
        public const string BODY_KEY = "AppUpdateRequired_Body";

        readonly IStringTableManager mStringTableManager;

        public AppUpdateRequiredPM( IStringTableManager i_stringTableManager ) {
            mStringTableManager = i_stringTableManager;

            SetVisibleProperty( false );
        }

        public void Show() {
            SetBodyTextProperty();
            SetTitleTextProperty();
            SetVisibleProperty( true );
        }

        private void SetBodyTextProperty() {
            string text = mStringTableManager.Get( BODY_KEY );
            ViewModel.SetProperty( BODY_TEXT_PROPERTY, text );
        }

        private void SetTitleTextProperty() {
            string text = mStringTableManager.Get( TITLE_KEY );
            ViewModel.SetProperty( TITLE_TEXT_PROPERTY, text );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}