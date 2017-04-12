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
            mBackend = i_backend;

            DownloadMonsterData();
        }

        public IMonsterData GetData( string i_id ) {
            foreach ( MonsterData data in AllMonsterData ) {
                if ( data.Id == i_id ) {
                    return data;
                }
            }

            return null;
        }

        private void DownloadMonsterData() {
            AllMonsterData = new List<MonsterData>();

            mBackend.GetTitleData( MONSTER_DATA_TITLE_KEY, ( result ) => {
                AllMonsterData = JsonConvert.DeserializeObject<List<MonsterData>>( result );
            } );
        }
    }
}