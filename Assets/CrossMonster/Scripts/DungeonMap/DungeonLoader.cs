using Zenject;
using MyLibrary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

namespace CrossMonsters {
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

        public void OnClick() {
            mLoadingPM.Show();
            GetGameSessionFromServer();
        }

        private void GetGameSessionFromServer() {
            Dictionary<string, string> cloudParams = new Dictionary<string, string>();
            cloudParams.Add( "AreaId", "0" );
            cloudParams.Add( "DungeonId", "0" );

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
