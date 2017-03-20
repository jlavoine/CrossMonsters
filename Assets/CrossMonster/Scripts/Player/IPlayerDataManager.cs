using MyLibrary;

namespace CrossMonsters {
    public interface IPlayerDataManager {
        void Init( IBasicBackend i_backend );

        int Gold { get; }
    }
}
