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
        ILoginMethod_Google GoogleLogin;

        [Inject]
        IBackendManager BackendManager;

        public void Authenticate() {
            UnityEngine.Debug.LogError( "About to authenticate with: " + PreferredLoginMethod.LoginMethod );

            EnsureValidLoginMethod();
            LogInWithMethod();
        }

        public void OnLogin() {
            IBasicBackend backend = BackendManager.GetBackend<IBasicBackend>();
            UnityEngine.Debug.LogError( "ONLOGIN with " + PreferredLoginMethod.LoginMethod );
            if ( PreferredLoginMethod.LoginMethod != LoginMethods.DeviceId ) {                
                UnityEngine.Debug.LogError( "Logged in with something other than device, so force linking device" );
                backend.LinkDeviceToAccount( (result) => { } );
            }

            backend.GetAccountInfo();
        }

        private void EnsureValidLoginMethod() {
            if ( !LoginMethodValidator.IsValid( PreferredLoginMethod.LoginMethod ) ) {
                PreferredLoginMethod.LoginMethod = LoginMethods.DeviceId;
            }
        }

        private void LogInWithMethod() {
            UnityEngine.Debug.LogError( "Logging in with " + PreferredLoginMethod.LoginMethod );

            switch ( PreferredLoginMethod.LoginMethod ) {
                case LoginMethods.GameCenter:
                    GameCenterLogin.Authenticate();
                    break;
                case LoginMethods.Google:                    
                    GoogleLogin.Authenticate();
                    break;
                default: // device
                    DeviceIdLogin.Authenticate();
                    break;
            }
        }
    }
}
