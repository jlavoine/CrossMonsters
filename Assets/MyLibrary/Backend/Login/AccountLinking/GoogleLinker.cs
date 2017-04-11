using Zenject;
using PlayFab.ClientModels;
using PlayFab;
using GooglePlayGames;
using UnityEngine;

namespace MyLibrary {
    public class GoogleLinker : IGoogleLinker {
        [Inject]
        IBackendManager BackendManager;

        public void AttemptLink( Callback<AccountLinkResultTypes> i_requestCallback, bool i_forceLink = false ) {
#if UNITY_EDITOR
            i_requestCallback( AccountLinkResultTypes.SuccessfulLink );
#else
            if ( !Social.localUser.authenticated ) {
                Social.localUser.Authenticate( (result) => {
                    OnAuthorizeAttempt( result, i_requestCallback, i_forceLink );
                } );
            } else {
                OnAuthorizeAttempt( true, i_requestCallback, i_forceLink );
            }            
#endif
        }

        private void OnAuthorizeAttempt( bool i_success, Callback<AccountLinkResultTypes> i_requestCallback, bool i_forceLink = false ) {
            if ( !i_success ) {
                i_requestCallback( AccountLinkResultTypes.Error );
                return;
            }

            PlayGamesPlatform.Instance.GetServerAuthCode( ( code, authToken ) => {
                PlayFabBackend backend = BackendManager.GetBackend<PlayFabBackend>();
                backend.StartRequest( "Linking account with Google with " + authToken );

                LinkGoogleAccountRequest request = new LinkGoogleAccountRequest() {
                    ServerAuthCode = authToken,
                    ForceLink = i_forceLink
                };

                PlayFabClientAPI.LinkGoogleAccount( request,
                    ( result ) => {
                        backend.RequestComplete( "LinkAccountToGoogle() complete, success", LogTypes.Info );
                        i_requestCallback( AccountLinkResultTypes.SuccessfulLink );
                    },
                    ( error ) => {
                        backend.HandleError( error, "LinkAccountToGoogle() complete, error" );
                        switch ( error.Error ) {
                            case PlayFabErrorCode.AccountAlreadyLinked:
                                i_requestCallback( AccountLinkResultTypes.AlreadyLinked );
                                break;
                            case PlayFabErrorCode.LinkedAccountAlreadyClaimed:
                                i_requestCallback( AccountLinkResultTypes.LinkAlreadyClaimed );
                                break;
                            default:
                                i_requestCallback( AccountLinkResultTypes.Error );
                                break;
                        }
                    } );
            } );
        }

        public void Unlink( Callback<bool> i_callback ) {
            PlayFabBackend backend = BackendManager.GetBackend<PlayFabBackend>();
            backend.StartRequest( "Unlinking account from Google" );

            UnlinkGoogleAccountRequest request = new UnlinkGoogleAccountRequest();

            PlayFabClientAPI.UnlinkGoogleAccount( request,
                ( result ) => {
                    backend.RequestComplete( "UnlinkGoogleFromAccount() complete, success", LogTypes.Info );
                    i_callback( true );
                },
                ( error ) => {
                    backend.HandleError( error, "UnlinkGoogleFromAccount() complete, error" );
                    i_callback( false );
                } );
        }
    }
}