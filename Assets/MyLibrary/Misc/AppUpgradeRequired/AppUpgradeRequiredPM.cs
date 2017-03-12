
namespace MyLibrary {
    public class AppUpgradeRequiredPM : PresentationModel, IAppUpgradeRequiredPM {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string BODY_TEXT_PROPERTY = "Body";
        public const string TITLE_TEXT_PROPERTY = "Title";

        public const string TITLE_KEY = "AppUpgradeRequired_Title";
        public const string BODY_KEY = "AppUpgradeRequired_Body";

        readonly IStringTableManager mStringTableManager;

        public AppUpgradeRequiredPM( IStringTableManager i_stringTableManager ) {
            UnityEngine.Debug.LogError( "When is this happening: " + i_stringTableManager );
            mStringTableManager = i_stringTableManager;

            SetVisibleProperty( false );
            SetBodyTextProperty();
            SetTitleTextProperty();
        }

        public void Show() {
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