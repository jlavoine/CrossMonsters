﻿using MyLibrary;

namespace MonsterMatch {
    public interface ITimedChestSaveData {
        void Init( IBasicBackend i_backend );
        
        bool IsChestAvailable( string i_id );
        bool CanOpenChest( ITimedChestData i_data );

        void OpenChest( ITimedChestData i_data, ITimedChestPM i_chestPM );

        int GetCurrentKeysForChest( string i_keyId );

        long GetNextAvailableTime( string i_id );
    }
}
