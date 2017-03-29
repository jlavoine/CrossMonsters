using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class LoginMethod_DeviceId : ILoginMethod_DeviceId {
        [Inject]
        IBackendManager Backend;

        public void Authenticate() {
            Backend.GetBackend<IBasicBackend>().Authenticate( SystemInfo.deviceUniqueIdentifier );
        }
    }
}
