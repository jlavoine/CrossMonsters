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

	public void OnClick() {
        UnityEngine.Debug.LogError( "-----------------Trying: " + BackendManager );
        if ( !Social.localUser.authenticated ) {
            Social.localUser.Authenticate( OnAuthed );
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

        } else {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            backend.LinkAccountToGameCenter( Social.localUser.id, OnLinkAttemptCallback );
        }
    }

    private void OnLinkAttemptCallback( bool i_success ) {
        SceneManager.LoadScene( "Login" );
    }
}
