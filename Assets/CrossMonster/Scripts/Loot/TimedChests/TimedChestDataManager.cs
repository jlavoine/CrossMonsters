using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace MonsterMatch {
    public class TimedChestDataManager : ITimedChestDataManager {
        public const string TITLE_KEY = "TimedChests";        

        private IBasicBackend mBackend;

        private List<ITimedChestData> mAllChestData;
        public List<ITimedChestData> TimedChestData { get { return mAllChestData; } set { mAllChestData = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadTimedChestData();
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
    }
}
