using Zenject;

namespace MyLibrary {
    public class LinkAccountButton : ILinkAccountButton {
        [Inject]
        IAccountAlreadyLinkedPM AccountAlreadyLinkedPM;

        [Inject]
        IAccountLinkDonePM AccountLinkDonePM;

        [Inject]
        IAccountLinker AccountLinker;

        [Inject]
        IPreferredLoginMethod PreferredLoginMethod;

        private LoginMethods mMethod;

        public void OnClick( LoginMethods i_loginMethod ) {            
            mMethod = i_loginMethod;
            AccountLinker.AttemptToLink( i_loginMethod, OnLinkResult );
        }

        private void OnLinkResult( AccountLinkResultTypes i_result ) {
            switch ( i_result ) {
                case AccountLinkResultTypes.Error:
                case AccountLinkResultTypes.AlreadyLinked:
                    ShowPopupWithSuccessResult( false );
                    break;
                case AccountLinkResultTypes.LinkAlreadyClaimed:
                    ShowAlreadyLinkedPopupWithLinkMethod();
                    break;
                case AccountLinkResultTypes.SuccessfulLink:                
                    ShowPopupWithSuccessResult( true );
                    break;
            }
        }

        private void ShowPopupWithSuccessResult( bool i_result ) {
            AccountLinkDonePM.SetLinkSuccess( i_result );
            AccountLinkDonePM.Show();
        }

        private void ShowAlreadyLinkedPopupWithLinkMethod() {
            AccountAlreadyLinkedPM.LinkMethod = this;
            AccountAlreadyLinkedPM.Show();
        }

        public void SetPreferredLoginMethod() {
            PreferredLoginMethod.LoginMethod = mMethod;
        }

        public void ForceLinkAccount() {
            AccountLinker.AttemptForceLink( mMethod, ( result ) => {
                ShowPopupWithSuccessResult( result );
            } );
        }
    }
}
