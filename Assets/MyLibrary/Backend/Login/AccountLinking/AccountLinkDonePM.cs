using Zenject;

namespace MyLibrary {
    public class AccountLinkDonePM : BasicWindowPM, IAccountLinkDonePM {
        public const string SUCCESS_KEY = "AccountLink_Success";
        public const string FAIL_KEY = "AccountLink_Fail";

        public const string TEXT_PROPERTY = "Text";

        [Inject]
        IStringTableManager StringTable;

        public AccountLinkDonePM() {
            SetVisibleProperty( false );
        }

        public void SetLinkSuccess( bool i_success ) {
            if ( i_success ) {
                SetTextPropertyWithKey( SUCCESS_KEY );
            } else {
                SetTextPropertyWithKey( FAIL_KEY );
            }
        }

        private void SetTextPropertyWithKey( string i_key ) {
            string text = StringTable.Get( i_key );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
