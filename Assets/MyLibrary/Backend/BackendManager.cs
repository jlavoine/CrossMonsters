
namespace MyLibrary {
    public class BackendManager : IBackendManager {
        private IBasicBackend mBackend;

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;
        }

        public T GetBackend<T>() {
            return (T) mBackend;
        }

        public string GetPlayerId() {
            return mBackend.PlayerId;
        }
    }
}