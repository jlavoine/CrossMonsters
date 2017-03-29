using Zenject;

namespace MyLibrary {
    public class LoginMethodManager : ILoginMethodManager {

        [Inject]
        IPreferredLoginMethod PreferredLoginMethod;

        [Inject]
        ILoginMethodValidator LoginMethodValidator;

        [Inject]
        ILoginMethod_DeviceId DeviceIdLogin;

        [Inject]
        ILoginMethod_GameCenter GameCenterLogin;

        [Inject]
        IBackendManager BackendManager;

        public void Authenticate() {
            UnityEngine.Debug.LogError( "About to authenticate with: " + PreferredLoginMethod.LoginMethod );

            EnsureValidLoginMethod();
            LogInWithMethod();
        }

        public void OnLogin() {
            if ( PreferredLoginMethod.LoginMethod != LoginMethods.DeviceId ) {
                IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
                backend.LinkDeviceToAccount( (result) => { } );
            }
        }

        private void EnsureValidLoginMethod() {
            if ( !LoginMethodValidator.IsValid( PreferredLoginMethod.LoginMethod ) ) {
                PreferredLoginMethod.LoginMethod = LoginMethods.DeviceId;
            }
        }

        private void LogInWithMethod() {
            switch ( PreferredLoginMethod.LoginMethod ) {
                case LoginMethods.GameCenter:
                    GameCenterLogin.Authenticate();
                    break;
                default: // device
                    DeviceIdLogin.Authenticate();
                    break;
            }
        }
    }
}
