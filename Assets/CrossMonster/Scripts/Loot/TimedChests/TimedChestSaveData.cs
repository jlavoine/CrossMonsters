using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;
using System;
using Zenject;

namespace MonsterMatch {
    public class TimedChestSaveData : ITimedChestSaveData {
        public const string SAVE_DATA_KEY = "TimedChestProgress";

        private IBasicBackend mBackend;

        [Inject]
        IPlayerInventoryManager Inventory;

        private Dictionary<string, ITimedChestSaveDataEntry> mSaveData;
        public Dictionary<string, ITimedChestSaveDataEntry> SaveData { get { return mSaveData; } set { mSaveData = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadTimedChestPlayerSaveData();
        }

        public void OpenChest( ITimedChestData i_data ) {
            RemoveKeysFromInventory( i_data );
            // contact server to open
            
        }

        public bool IsChestAvailable( string i_id ) {
            if ( SaveData.ContainsKey( i_id ) ) {
                DateTime nextAvailable = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
                nextAvailable = nextAvailable.AddMilliseconds( SaveData[i_id].GetNextAvailableTime() );
                DateTime backendTime = mBackend.GetDateTime();
                return backendTime >= nextAvailable;
            } else {
                return false;
            }
        }

        public bool CanOpenChest( ITimedChestData i_data ) {
            int keysRequired = i_data.GetKeysRequired();
            int keysOwned = GetCurrentKeysForChest( i_data.GetKeyId() );

            return keysOwned >= keysRequired;
        }

        public long GetNextAvailableTime( string i_id ) {
            if ( SaveData.ContainsKey( i_id ) ) {
                return (long)SaveData[i_id].GetNextAvailableTime();
            }
            else {
                return 0;
            }
        }

        public int GetCurrentKeysForChest( string i_keyId ) {
            return Inventory.GetItemCount( i_keyId );
        }

        private void DownloadTimedChestPlayerSaveData() {
            SaveData = new Dictionary<string, ITimedChestSaveDataEntry>();

            mBackend.GetReadOnlyPlayerData( SAVE_DATA_KEY, ( result ) => {
                Dictionary<string, TimedChestSaveDataEntry> data = JsonConvert.DeserializeObject<Dictionary<string, TimedChestSaveDataEntry>>( result );

                foreach ( KeyValuePair<string, TimedChestSaveDataEntry> kvp in data ) {
                    SaveData.Add( kvp.Key, kvp.Value );
                }
            } );
        }

        private void RemoveKeysFromInventory( ITimedChestData i_data ) {
            Inventory.RemoveUsesFromItem( i_data.GetKeyId(), i_data.GetKeysRequired() );
        }
    }
}