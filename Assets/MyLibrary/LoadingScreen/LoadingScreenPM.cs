
namespace MyLibrary {
    public class LoadingScreenPM : PresentationModel, ILoadingScreenPM {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string TEXT_PROPERTY = "LoadingText";

        public const string LOADING_TEXT_KEY = "LOADING_TEXT";

        readonly IStringTableManager mStringTableManager;

        public LoadingScreenPM( IStringTableManager i_stringTableManager ) {
            mStringTableManager = i_stringTableManager;

            SetVisibleProperty( false );
            SetTextProperty();
        }

        protected override void _Dispose() {
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

        private void SetTextProperty() {
            string text = mStringTableManager.Get( LOADING_TEXT_KEY );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
