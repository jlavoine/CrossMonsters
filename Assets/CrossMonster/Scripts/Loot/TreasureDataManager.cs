using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace CrossMonsters {
    public class TreasureDataManager : ITreasureDataManager {
        public const string TREASURE_DATA_TITLE_KEY = "TreasureData";

        private IBasicBackend mBackend;

        private List<TreasureData> mAllTreasure;

        public void Init( IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing treasure data manager" );
            mBackend = i_backend;

            DownloadTreasureData();
        }

        private void DownloadTreasureData() {
            mAllTreasure = new List<TreasureData>();

            mBackend.GetTitleData( TREASURE_DATA_TITLE_KEY, ( result ) => {
                mAllTreasure = JsonConvert.DeserializeObject<List<TreasureData>>( result );

                foreach ( TreasureData td in mAllTreasure ) {
                    UnityEngine.Debug.LogError( td.Id );
                }
            } );
        }
    }
}
