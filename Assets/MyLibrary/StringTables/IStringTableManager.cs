
namespace MyLibrary {
    public interface IStringTableManager {
        void Init( string i_language, IBasicBackend i_backend );
        string Get( string i_key );
    }
}
