using Zenject;

namespace MyLibrary {
    public class AccountAlreadyLinkedPM : BasicWindowPM, IAccountAlreadyLinkedPM {
        [Inject]
        ISceneManager SceneManager;

        [Inject]
        IRemoveAccountLinkPM RemoveLinkPM;

        readonly IStringTableManager mStringTable;

        public const string TEXT_KEY = "AccountLink_AlreadyLinked";

        public const string TEXT_PROPERTY = "Text";

        private ILinkAccountButton mLinkMethod;
        public ILinkAccountButton LinkMethod { get { return mLinkMethod; } set { mLinkMethod = value; } }

        public AccountAlreadyLinkedPM( IStringTableManager i_stringTable ) {
            mStringTable = i_stringTable;

            SetTextProperty();
            SetVisibleProperty( false );
        }

        public void UseCurrentSave() {
            LinkMethod.ForceLinkAccount();
            Hide();
        }

        public void UseExistingSave() {
            LinkMethod.SetPreferredLoginMethod();
            SceneManager.LoadScene( "Login" );
        }

        public void InitiateRemoveLink() {
            RemoveLinkPM.LinkMethod = LinkMethod;
            RemoveLinkPM.Show();
            Hide();
        }

        private void SetTextProperty() {
            string text = mStringTable.Get( TEXT_KEY );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
