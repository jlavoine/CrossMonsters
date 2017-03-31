
namespace MyLibrary {
    public class RemoveDeviceLinkPM : BasicWindowPM, IRemoveDeviceLinkPM {
        public const string TEXT_KEY = "AccountLink_RemoveDevice";

        public const string TEXT_PROPERTY = "Text";

        readonly IStringTableManager mStringTable;
        readonly IBackendManager mBackendManager;
        readonly ISceneManager mSceneManager;

        public RemoveDeviceLinkPM( IStringTableManager i_stringTable, IBackendManager i_backendManager, ISceneManager i_sceneManager ) {
            mStringTable = i_stringTable;
            mBackendManager = i_backendManager;
            mSceneManager = i_sceneManager;

            SetTextProperty();
            Hide();
        }

        public void RemoveDeviceFromAccount() {
            IBasicBackend backend = mBackendManager.GetBackend<IBasicBackend>();
            backend.UnlinkDeviceFromAccount( (result) => { } );

            Hide();

            mSceneManager.LoadScene( "Login" );
        }

        private void SetTextProperty() {
            string text = mStringTable.Get( TEXT_KEY );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
