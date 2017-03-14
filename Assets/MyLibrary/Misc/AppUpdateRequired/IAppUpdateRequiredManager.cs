
namespace MyLibrary {
    public interface IAppUpdateRequiredManager {
        void Init( IBasicBackend i_backend );
        void TriggerUpgradeViewIfRequired();

        bool IsUpgradeRequired();
    }
}