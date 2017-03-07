using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace CrossMonsters {
    public class TreasureDataManager : ITreasureDataManager {
        public const string TREASURE_DATA_TITLE_KEY = "TreasureData";
        public const string TREASURE_SETS_TITLE_KEY = "TreasureSets";
        public const string TREASURE_PROGRESS_KEY = "TreasureProgress";

        private IBasicBackend mBackend;

        private List<TreasureData> mAllTreasure;
        private List<TreasureSetData> mAllTreasureSets;
        private List<string> mPlayerTreasure;

        public void Init( IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing treasure data manager" );
            mBackend = i_backend;

            DownloadTreasureData();
            DownloadTreasureSets();
            DownloadPlayerTreasure();
        }

        private void DownloadTreasureData() {
            mAllTreasure = new List<TreasureData>();

            mBackend.GetTitleData( TREASURE_DATA_TITLE_KEY, ( result ) => {
                mAllTreasure = JsonConvert.DeserializeObject<List<TreasureData>>( result );
            } );
        }

        private void DownloadTreasureSets() {
            mAllTreasureSets = new List<TreasureSetData>();

            mBackend.GetTitleData( TREASURE_SETS_TITLE_KEY, ( result ) => {
                mAllTreasureSets = JsonConvert.DeserializeObject<List<TreasureSetData>>( result );
            } );
        }

        private void DownloadPlayerTreasure() {
            mPlayerTreasure = new List<string>();

            mBackend.GetPlayerData( TREASURE_PROGRESS_KEY, ( result ) => {
                mPlayerTreasure = JsonConvert.DeserializeObject<List<string>>( result );
            } );
        }
    }
}
