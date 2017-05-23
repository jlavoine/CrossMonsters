using System.Collections.Generic;
using System;

namespace MyLibrary {
    public class BackendManager : IBackendManager {
        private IBasicBackend mBackend;

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;
        }

        public void MakeCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback ) {
            mBackend.MakeCloudCall( i_methodName, i_params, i_requestSuccessCallback );
        }

        public T GetBackend<T>() {
            return (T) mBackend;
        }

        public string GetPlayerId() {
            return mBackend.PlayerId;
        }

        public long GetTimeInMs() {
            DateTime curTime = mBackend.GetDateTime();
            return (long)curTime.Subtract( new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ) ).TotalMilliseconds;
        }
    }
}