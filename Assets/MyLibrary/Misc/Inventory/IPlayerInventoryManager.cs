
namespace MyLibrary {
    public interface IPlayerInventoryManager {
        void Init( IBasicBackend i_backend );

        int GetItemCount( string i_itemId );
    }
}