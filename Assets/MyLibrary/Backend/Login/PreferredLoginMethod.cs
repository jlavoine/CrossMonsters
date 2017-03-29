
namespace MyLibrary {
    public class PreferredLoginMethod : IPreferredLoginMethod {
        private LoginMethods mLoginMethod = LoginMethods.DeviceId;
        public LoginMethods LoginMethod { get { return mLoginMethod; } set { mLoginMethod = value; } }
    }
}
