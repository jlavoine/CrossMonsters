using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace MonsterMatch {
    public class TimedChestDataManager : ITimedChestDataManager {
        public const string TITLE_KEY = "TimedChests";
        public const string SAVE_DATA_KEY = "TimedChestProgress";

        private IBasicBackend mBackend;

        private List<ITimedChestData> mAllChestData;
        public List<ITimedChestData> TimedChestData { get { return mAllChestData; } set { mAllChestData = value; } }

        private Dictionary<string, ITimedChestSaveData> mSaveData;
        public Dictionary<string, ITimedChestSaveData> SaveData { get { return mSaveData; } set { mSaveData = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadTimedChestData();
            DownloadTimedChestPlayerSaveData();
        }

        private void DownloadTimedChestData() {
            TimedChestData = new List<ITimedChestData>();

            mBackend.GetTitleData( TITLE_KEY, ( result ) => {
                List<TimedChestData> allData = JsonConvert.DeserializeObject<List<TimedChestData>>( result );
                                
                foreach ( TimedChestData data in allData ) {
                    TimedChestData.Add( data );
                }
            } );
        }

        private void DownloadTimedChestPlayerSaveData() {
            SaveData = new Dictionary<string, ITimedChestSaveData>();

            mBackend.GetReadOnlyPlayerData( SAVE_DATA_KEY, ( result ) => {
                Dictionary<string, TimedChestSaveData> data = JsonConvert.DeserializeObject<Dictionary<string, TimedChestSaveData>>( result );

                foreach ( KeyValuePair<string, TimedChestSaveData> kvp in data ) {
                    SaveData.Add( kvp.Key, kvp.Value );
                }
            } );
        }
    }
}
