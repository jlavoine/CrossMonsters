using System.Collections.Generic;

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
    }
}