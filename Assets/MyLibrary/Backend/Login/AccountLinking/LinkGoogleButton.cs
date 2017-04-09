﻿using System;
using UnityEngine;
using Zenject;
using GooglePlayGames;

namespace MyLibrary {
    public class LinkGoogleButton : LinkAccountButton, ILinkGoogleButton {
        [Inject]
        IBackendManager BackendManager;

        [Inject]
        IPreferredLoginMethod PreferredLoginMethod;

        protected override void Authorize() {
            UnityEngine.Debug.LogError( "About to authorize" );
#if UNITY_EDITOR
            OnAlreadyLinkedCheck( true );
            //OnLinkAttemptResult( false );
#else
            if ( !Social.localUser.authenticated ) {
            UnityEngine.Debug.LogError( "Not authed, doing it" );
                Social.localUser.Authenticate( OnAuthorizeAttempt );
            } else {
                OnAuthorizeAttempt( true );
            }
#endif
        }

        protected override void OnSuccessfulAuth() {
            UnityEngine.Debug.LogError( "succesfully authed" );
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.IsAccountLinkedWithGoogle( Social.localUser.id, OnAlreadyLinkedCheck, OnLinkCheckError );
        }

        protected override void LinkAccount() {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            PlayGamesPlatform.Instance.GetServerAuthCode( ( code, authToken ) => {
                backend.LinkAccountToGoogle( authToken, OnLinkAttemptResult );
            } );            
        }

        public override void ForceLinkAccount() {
#if UNITY_EDITOR
            OnLinkAttemptResult( true );
#else
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.LinkAccountToGoogle( Social.localUser.id, OnLinkAttemptResult, true );
#endif
        }

        public override void UnlinkAccount() {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.UnlinkGoogleFromAccount();
        }

        public void SetPreferredLoginMethod() {
            PreferredLoginMethod.LoginMethod = LoginMethods.Google;
        }
    }
}