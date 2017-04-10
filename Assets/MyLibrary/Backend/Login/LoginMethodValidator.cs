using UnityEngine;

namespace MyLibrary {
    public class LoginMethodValidator : ILoginMethodValidator {

        public bool IsValid( LoginMethods i_method ) {
            if ( i_method == LoginMethods.DeviceId ) {
                return true;
            }  else {
                UnityEngine.Debug.LogError( "Checking is authed: " + Social.localUser.authenticated );
                return Social.localUser.authenticated;
            }
        }
    }
}
