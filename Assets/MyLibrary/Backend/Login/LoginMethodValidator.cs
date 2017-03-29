using UnityEngine;

namespace MyLibrary {
    public class LoginMethodValidator : ILoginMethodValidator {

        public bool IsValid( LoginMethods i_method ) {
            if ( i_method == LoginMethods.DeviceId ) {
                return true;
            } else if ( i_method == LoginMethods.GameCenter ) {
                return Social.localUser.authenticated;
            } else {
                return false;
            }
        }
    }
}
