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

        protected void OnAuthorizeAttempt( bool i_success ) {
            if ( i_success ) {

            } else {
                AccountLinkDonePM.Show(); // + error
            }
        }

        protected void OnAlreadyLinkedCheck( bool i_alreadyLinked ) {
            if ( i_alreadyLinked ) {

            } else {

            }
        }

        protected void OnLinkAttemptResult( bool i_success ) {
            if ( i_success ) {
                AccountAlreadyLinkedPM.Show();
            } else {

            }
        }
    }
}
