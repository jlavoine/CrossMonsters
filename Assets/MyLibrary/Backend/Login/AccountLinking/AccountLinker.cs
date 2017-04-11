using Zenject;

namespace MyLibrary {
    public class AccountLinker : IAccountLinker {
        [Inject]
        IGoogleLinker GoogleLinker;

        public void AttemptToLink( LoginMethods i_linkType, Callback<AccountLinkResultTypes> i_callback ) {
            switch ( i_linkType ) {
                case LoginMethods.Google:
                    GoogleLinker.AttemptLink( i_callback );
                    break;
            }
        }

        public void AttemptForceLink( LoginMethods i_linkType, Callback<bool> i_callback ) {
            bool forceLink = true;

            switch ( i_linkType ) {
                case LoginMethods.Google:
                    GoogleLinker.AttemptLink( ( result ) => {
                        bool success = result == AccountLinkResultTypes.SuccessfulLink;
                        i_callback( success );
                    }, forceLink );
                    break;
            }
        }

        public void Unlink( LoginMethods i_linkType, Callback<bool> i_callback ) {
            switch ( i_linkType ) {
                case LoginMethods.Google:
                    GoogleLinker.Unlink( i_callback );
                    break;
            }
        }
    }
}
