using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;
using Zenject;

namespace MonsterMatch {
    public class TreasureDataManager : ITreasureDataManager {
        public const string TREASURE_DATA_TITLE_KEY = "TreasureData";
        public const string TREASURE_SETS_TITLE_KEY = "TreasureSets";
        public const string TREASURE_PROGRESS_KEY = "TreasureProgress";
        public const string TREASURE_VALUE_KEY = "TreasureRarityValues";

        private IBasicBackend mBackend;

        [Inject]
        ITreasureSpawner TreasureSpawner;

        private Dictionary<string, ITreasureData> mAllTreasure = new Dictionary<string, ITreasureData>();
        private List<ITreasureSetData> mAllTreasureSets;
        private Dictionary<string, ITreasure> mPlayerTreasure;
        private Dictionary<string, int> mTreasureValue = new Dictionary<string, int>();

        public Dictionary<string, ITreasureData> AllTreasure { get { return mAllTreasure; } set { mAllTreasure = value; } }
        public List<ITreasureSetData> TreasureSetData { get { return mAllTreasureSets; } set { mAllTreasureSets = value; } }
        public Dictionary<string, ITreasure> PlayerTreasure { get { return mPlayerTreasure; } set { mPlayerTreasure = value; } }
        public Dictionary<string, int> TreasureValues { get { return mTreasureValue; } set { mTreasureValue = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadTreasureData_ThenPlayerTreasureData();
            DownloadTreasureSets();
            DownloadTreasureValues();                    
        }

        public bool DoesPlayerHaveTreasure( string i_treasureId ) {
            return PlayerTreasure.ContainsKey( i_treasureId );
        }

        public int GetValueForRarity( string i_rarity ) {
            if ( TreasureValues.ContainsKey( i_rarity ) ) {
                return TreasureValues[i_rarity];
            } else {
                return 0;
            }
        }

        public ITreasureData GetTreasureDataForId( string i_id ) {
            if ( AllTreasure.ContainsKey( i_id ) ) {
                return AllTreasure[i_id];
            } else {
                return null;
            }
        }

        private void DownloadTreasureData_ThenPlayerTreasureData() {
            mBackend.GetTitleData( TREASURE_DATA_TITLE_KEY, ( result ) => {
                List<TreasureData> allData = JsonConvert.DeserializeObject<List<TreasureData>>( result );
                foreach ( TreasureData data in allData ) {
                    AllTreasure.Add( data.GetId(), data );
                }

                DownloadAndCreatePlayerTreasure();
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
                TreasureValues = JsonConvert.DeserializeObject<Dictionary<string, int>>( result );
            } );
        }

        private void DownloadAndCreatePlayerTreasure() {
            mBackend.GetReadOnlyPlayerData( TREASURE_PROGRESS_KEY, ( result ) => {
                List<string> treasureIds = JsonConvert.DeserializeObject<List<string>>( result );
                CreatePlayerTreasure( treasureIds );
            } );
        }

        private void CreatePlayerTreasure( List<string> i_treasureIds ) {
            foreach ( string treasureId in i_treasureIds ) {
                ITreasureData data = GetTreasureDataForId( treasureId );
                if ( data != null ) {
                    ITreasure treasure = TreasureSpawner.Create( data );
                    PlayerTreasure.Add( treasureId, treasure );
                }
            }
        }
    }
}
