using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace MonsterMatch {
    public class TreasureDataManager : ITreasureDataManager {
        public const string TREASURE_DATA_TITLE_KEY = "TreasureData";
        public const string TREASURE_SETS_TITLE_KEY = "TreasureSets";
        public const string TREASURE_PROGRESS_KEY = "TreasureProgress";
        public const string TREASURE_VALUE_KEY = "TreasureRarityValues";

        private IBasicBackend mBackend;

        private List<TreasureData> mAllTreasure;
        private List<ITreasureSetData> mAllTreasureSets;
        private List<string> mPlayerTreasure;
        private Dictionary<string, int> mTreasureValue = new Dictionary<string, int>();

        public List<TreasureData> AllTreasure { get { return mAllTreasure; } set { mAllTreasure = value; } }
        public List<ITreasureSetData> TreasureSetData { get { return mAllTreasureSets; } set { mAllTreasureSets = value; } }
        public List<string> PlayerTreasure { get { return mPlayerTreasure; } set { mPlayerTreasure = value; } }
        public Dictionary<string, int> TreasureValues { get { return mTreasureValue; } set { mTreasureValue = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadTreasureData();
            DownloadTreasureSets();
            DownloadTreasureValues();
            DownloadPlayerTreasure();            
        }

        public bool DoesPlayerHaveTreasure( string i_treasureId ) {
            return PlayerTreasure.Contains( i_treasureId );
        }

        public int GetValueForRarity( string i_rarity ) {
            if ( TreasureValues.ContainsKey( i_rarity ) ) {
                return TreasureValues[i_rarity];
            } else {
                return 0;
            }
        }

        private void DownloadTreasureData() {
            mAllTreasure = new List<TreasureData>();

            mBackend.GetTitleData( TREASURE_DATA_TITLE_KEY, ( result ) => {
                mAllTreasure = JsonConvert.DeserializeObject<List<TreasureData>>( result );
            } );
        }

        private void DownloadTreasureSets() {
            mAllTreasureSets = new List<ITreasureSetData>();

            mBackend.GetTitleData( TREASURE_SETS_TITLE_KEY, ( result ) => {
                List<TreasureSetData> setData = JsonConvert.DeserializeObject<List<TreasureSetData>>( result );

                foreach( TreasureSetData set in setData) {
                    mAllTreasureSets.Add( set );
                }
            } );
        }

        private void DownloadTreasureValues() {
            mBackend.GetTitleData( TREASURE_VALUE_KEY, ( result ) => {
                Dictionary<string, int> values = JsonConvert.DeserializeObject<Dictionary<string, int>>( result );
            } );
        }

        private void DownloadPlayerTreasure() {
            mPlayerTreasure = new List<string>();

            mBackend.GetReadOnlyPlayerData( TREASURE_PROGRESS_KEY, ( result ) => {
                mPlayerTreasure = JsonConvert.DeserializeObject<List<string>>( result );
            } );
        }
    }
}
