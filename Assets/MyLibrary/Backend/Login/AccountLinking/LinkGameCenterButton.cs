using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MyLibrary;
using UnityEngine.SceneManagement;

/// <summary>
/// TEMPORARY.
/// </summary>

public class LinkGameCenterButton {
    [Inject]
    IBackendManager BackendManager;

    [Inject]
    IAccountAlreadyLinkedPM AccountAlreadyLinkedPM;

    [Inject]
    IAccountLinkDonePM AccountLinkDonePM;

	public void OnClick() {
        if ( !Social.localUser.authenticated ) {
#if UNITY_EDITOR
            //OnIsLinkedCallback( true );
            OnLinkAttemptCallback( true );
#else
            Social.localUser.Authenticate( OnAuthed );
#endif
        } else {
            OnAuthed( true );
        }
    }

    private void OnAuthed( bool i_success ) {
        if ( i_success ) {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.IsAccountLinkedWithGameCenter( Social.localUser.id, OnIsLinkedCallback );
        }
    }

    private void OnIsLinkedCallback( bool i_isLinked ) {
        if ( i_isLinked ) {
            AccountAlreadyLinkedPM.Show();
        } else {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.LinkAccountToGameCenter( Social.localUser.id, OnLinkAttemptCallback );
        }
    }

    private void OnLinkAttemptCallback( bool i_success ) {
        if ( i_success ) {
            AccountLinkDonePM.Show();
        } else {

        }
    }
}
