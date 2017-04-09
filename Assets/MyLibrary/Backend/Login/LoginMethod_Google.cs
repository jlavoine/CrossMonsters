using Zenject;
using UnityEngine;
using GooglePlayGames;

namespace MyLibrary {
    public class LoginMethod_Google : ILoginMethod_Google {
        [Inject]
        IBackendManager Backend;

        public void Authenticate() {
            UnityEngine.Debug.LogError( "Authing with google" );            
            PlayGamesPlatform.Instance.GetServerAuthCode( ( code, authToken ) => {
                Backend.GetBackend<IBasicBackend>().AuthenticateWithGoogle( authToken );
            } );
        }
    }
}
