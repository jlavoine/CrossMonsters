using MyLibrary;

namespace MonsterMatch {
    public interface ITimedChestSaveData {
        void Init( IBasicBackend i_backend );
        
        bool IsChestAvailable( string i_id );

        int GetCurrentKeysForChest( string i_keyId );
    }
}
