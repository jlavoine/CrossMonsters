using System.Collections.Generic;

namespace MyLibrary {
    public interface IBackendManager {
        T GetBackend<T>();

        void Init( IBasicBackend i_backend );
        void MakeCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback );

        string GetPlayerId();
    }
}
