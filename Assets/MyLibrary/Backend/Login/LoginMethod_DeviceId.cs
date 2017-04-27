using Zenject;
using UnityEngine;

namespace MyLibrary {
    public class LoginMethod_DeviceId : ILoginMethod_DeviceId {
        [Inject]
        IBackendManager Backend;

        public void Authenticate() {
            //UnityEngine.Debug.LogError( "Authing with " + SystemInfo.deviceUniqueIdentifier );
            Backend.GetBackend<IBasicBackend>().Authenticate( SystemInfo.deviceUniqueIdentifier );
        }
    }
}
