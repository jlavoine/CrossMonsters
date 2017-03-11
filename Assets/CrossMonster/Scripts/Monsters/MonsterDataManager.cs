using System.Collections.Generic;
using MyLibrary;
using Newtonsoft.Json;

namespace CrossMonsters {
    public class MonsterDataManager : IMonsterDataManager {
        public const string MONSTER_DATA_TITLE_KEY = "MonsterData";

        private IBasicBackend mBackend;

        private List<MonsterData> mAllMonsterData;
        public List<MonsterData> AllMonsterData { get { return mAllMonsterData; } set { mAllMonsterData = value; } }

        public void Init( IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing monster data manager" );
            mBackend = i_backend;

            DownloadMonsterData();
        }

        private void DownloadMonsterData() {
            AllMonsterData = new List<MonsterData>();

            mBackend.GetTitleData( MONSTER_DATA_TITLE_KEY, ( result ) => {
                AllMonsterData = JsonConvert.DeserializeObject<List<MonsterData>>( result );
                foreach ( MonsterData md in AllMonsterData ) {
                    UnityEngine.Debug.LogError( md.Id );
                }
            } );
        }
    }
}