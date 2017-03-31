
namespace MyLibrary {
    public class RemoveAccountLinkPM : BasicWindowPM, IRemoveAccountLinkPM {
        public const string TEXT_KEY = "AccountLink_Unlink";

        public const string TEXT_PROPERTY = "Text";

        readonly IStringTableManager mStringTable;

        private ILinkAccountButton mLinkMethod;
        public ILinkAccountButton LinkMethod { get { return mLinkMethod; } set { mLinkMethod = value; } }

        public RemoveAccountLinkPM( IStringTableManager i_stringTable ) {
            mStringTable = i_stringTable;

            SetVisibleProperty( false );
            SetTextProperty();
        }

        public void AttemptToUnlinkAccount() {
            SetVisibleProperty( false );
            LinkMethod.UnlinkAccount();
        }

        private void SetTextProperty() {
            string text = mStringTable.Get( TEXT_KEY );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}