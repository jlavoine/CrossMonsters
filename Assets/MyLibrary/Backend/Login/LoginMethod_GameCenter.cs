using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class LoginMethod_GameCenter : ILoginMethod_GameCenter {
        [Inject]
        IBackendManager Backend;

        public void Authenticate() {
            Backend.GetBackend<IBasicBackend>().Authenticate( Social.localUser.id );
        }
    }
}
