
namespace MyLibrary {
    public interface IAppUpgradeRequiredManager {
        void Init( IBasicBackend i_backend );
        void TriggerUpgradeViewIfRequired();

        bool IsUpgradeRequired();
    }
}