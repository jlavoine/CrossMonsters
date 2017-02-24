
namespace MyLibrary {
    public interface IBackendManager {
        T GetBackend<T>();

        void Init( IBasicBackend i_backend );

        string GetPlayerId();
    }
}
