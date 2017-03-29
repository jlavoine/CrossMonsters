using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class LinkGameCenterButton : LinkAccountButton, ILinkGameCenterButton {
        [Inject]
        IBackendManager BackendManager;

        protected override void Authorize() {
#if UNITY_EDITOR
            //OnIsLinkedCallback( true );
            OnLinkAttemptResult( false );
#else
            if ( !Social.localUser.authenticated ) {
                Social.localUser.Authenticate( OnAuthorizeAttempt );
            } else {
                OnAuthorizeAttempt( true );
            }
#endif
        }

        protected override void OnSuccessfulAuth() {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.IsAccountLinkedWithGameCenter( Social.localUser.id, OnAlreadyLinkedCheck, OnLinkCheckError );
        }

        protected override void LinkAccount() {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.LinkAccountToGameCenter( Social.localUser.id, OnLinkAttemptResult );
        }
    }
}