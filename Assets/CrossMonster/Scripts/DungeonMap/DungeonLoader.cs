using Zenject;
using MyLibrary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

namespace MonsterMatch {
    public class DungeonLoader {
        readonly ILoadingScreenPM mLoadingPM;
        readonly IBackendManager mBackendManager;
        readonly ICurrentDungeonGameManager mCurrentDungeonData;
        readonly ISceneManager mSceneManager;

        public DungeonLoader( ILoadingScreenPM i_loadingPM, IBackendManager i_backend, ICurrentDungeonGameManager i_currentDungeon, ISceneManager i_sceneManager ) {
            mLoadingPM = i_loadingPM;
            mBackendManager = i_backend;
            mCurrentDungeonData = i_currentDungeon;
            mSceneManager = i_sceneManager;
        }

        public void OnClick( string i_gameType, int i_areaId, int i_dungeonId ) {
            mLoadingPM.Show();
            GetGameSessionFromServer( i_gameType, i_areaId, i_dungeonId );
        }

        private void GetGameSessionFromServer( string i_gameType, int i_areaId, int i_dungeonId ) {
            Dictionary<string, string> cloudParams = new Dictionary<string, string>();
            cloudParams.Add( "AreaId", i_areaId.ToString() );
            cloudParams.Add( "DungeonId", i_dungeonId.ToString() );
            cloudParams.Add( "GameType", i_gameType );

            mBackendManager.GetBackend<IBasicBackend>().MakeCloudCall( BackendMethods.GET_DUNGEON_SESSION, cloudParams, ( result ) => {
                OnDungeonGameSessionResponse( JsonConvert.DeserializeObject<DungeonGameSessionData>( result["data"] ) );
            } );
        }

        public void OnDungeonGameSessionResponse( IDungeonGameSessionData i_data ) {           
            mCurrentDungeonData.SetData( i_data );
            mSceneManager.LoadScene( "Combat" );
        }

        public class Factory : Factory<DungeonLoader> { }
    }
}
