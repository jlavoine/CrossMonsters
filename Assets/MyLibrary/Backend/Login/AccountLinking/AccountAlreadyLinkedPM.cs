
namespace MyLibrary {
    public class AccountAlreadyLinkedPM : BasicWindowPM, IAccountAlreadyLinkedPM {
        readonly IStringTableManager mStringTable;

        public const string TEXT_KEY = "AccountLink_AlreadyLinked";

        public const string TEXT_PROPERTY = "Text";

        public AccountAlreadyLinkedPM( IStringTableManager i_stringTable ) {
            mStringTable = i_stringTable;

            SetTextProperty();
            SetVisibleProperty( false );
        }

        private void SetTextProperty() {
            string text = mStringTable.Get( TEXT_KEY );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
