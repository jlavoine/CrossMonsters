using Zenject;

namespace MyLibrary {
    public abstract class LinkAccountButton {
        protected abstract void Authorize();
        protected abstract void OnSuccessfulAuth();
        protected abstract void LinkAccount();

        [Inject]
        IAccountAlreadyLinkedPM AccountAlreadyLinkedPM;

        [Inject]
        IAccountLinkDonePM AccountLinkDonePM;

        public void OnClick() {
            Authorize();
        }

        public void OnAuthorizeAttempt( bool i_success ) {
            if ( i_success ) {
                OnSuccessfulAuth();
            } else {
                ShowPopupWithSuccessResult( false );                
            }
        }

        public void OnAlreadyLinkedCheck( bool i_alreadyLinked ) {
            if ( i_alreadyLinked ) {

            } else {
                LinkAccount();
            }
        }

        public void OnLinkCheckError() {
            ShowPopupWithSuccessResult( false );
        }

        public void OnLinkAttemptResult( bool i_success ) {
            if ( i_success ) {
                ShowPopupWithSuccessResult( true );
            } else {
                ShowPopupWithSuccessResult( false );
            }
        }

        private void ShowPopupWithSuccessResult( bool i_result ) {
            AccountLinkDonePM.SetLinkSuccess( i_result );
            AccountLinkDonePM.Show();
        }
    }
}
