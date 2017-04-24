using MyLibrary;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace MonsterMatch {
    public class CrossBackend : PlayFabBackend, ICrossBackend {
        public CrossBackend() : base() { }

        private DateTime mServerLoginTime;
        private DateTime mClientLoginTime;  

        public void SetLoggedInTime( double i_serverTimeFromEpoch ) {
            mClientLoginTime = DateTime.UtcNow;

            mServerLoginTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            mServerLoginTime = mServerLoginTime.AddMilliseconds( i_serverTimeFromEpoch );
        }

        public override DateTime GetDateTime() {
            TimeSpan timeFromClientLoginToNow = DateTime.UtcNow - mClientLoginTime;
            DateTime now = mServerLoginTime + timeFromClientLoginToNow;

            return now;
        }

        #region Game cloud calls
        // these cloud calls must be made ONE AT A TIME to avoid the server processing them too closely together.
        // if this happens, the server may increment the wrong values at the wrong times. i.e. two upgrade calls to
        // close together will both see the current level as 1, and change it to 2, when the first all should change it
        // from 1->2 and the second from 2->3
        #endregion

        /*private double GetClientTimestamp() {
            return ( DateTime.UtcNow - mLoggedInTime ).TotalMilliseconds;
        }*/

        #region Wait-for-game calls
        // these are calls that are important and rely on all previous game calls to be complete.
        // this is because the game calls are actually processed on the client after sending them so the
        // player doesn't have to wait. But these calls require the state of the server to be up-to-date before
        // sending.   
        #endregion
    }
}