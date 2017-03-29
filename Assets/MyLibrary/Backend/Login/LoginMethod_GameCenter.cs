using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class LoginMethod_GameCenter : ILoginMethod_DeviceId {
        [Inject]
        IBackendManager Backend;

        public void Authenticate() {
            Backend.GetBackend<IBasicBackend>().Authenticate( Social.localUser.id );
        }
    }
}
