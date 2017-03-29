using Zenject;

namespace MyLibrary {
    public class LoginMethodManager : ILoginMethodManager {

        [Inject]
        IPreferredLoginMethod PreferredLoginMethod;

        [Inject]
        ILoginMethodValidator LoginMethodValidator;

        [Inject]
        ILoginMethod_DeviceId DeviceIdLogin;

        public void Authenticate() {
            EnsureValidLoginMethod();
            LogInWithMethod();
        }

        private void EnsureValidLoginMethod() {
            if ( !LoginMethodValidator.IsValid( PreferredLoginMethod.LoginMethod ) ) {
                PreferredLoginMethod.LoginMethod = LoginMethods.DeviceId;
            }
        }

        private void LogInWithMethod() {
            switch ( PreferredLoginMethod.LoginMethod ) {
                default: // device
                    DeviceIdLogin.Authenticate();
                    break;
            }
        }
    }
}
