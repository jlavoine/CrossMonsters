using System;
using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class LinkGameCenterButton : LinkAccountButton, ILinkGameCenterButton {
        [Inject]
        IBackendManager BackendManager;

        [Inject]
        IPreferredLoginMethod PreferredLoginMethod;

        protected override void Authorize() {
#if UNITY_EDITOR
            OnAlreadyLinkedCheck( true );
            //OnLinkAttemptResult( false );
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

        public override void ForceLinkAccount() {
#if UNITY_EDITOR
            OnLinkAttemptResult( true );
#else
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.LinkAccountToGameCenter( Social.localUser.id, OnLinkAttemptResult, true );
#endif
        }

        public void SetPreferredLoginMethod() {
            PreferredLoginMethod.LoginMethod = LoginMethods.GameCenter;
        }
    }
}