using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MyLibrary;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;
using Zenject;

namespace CrossMonsters {
    public class LoginScreen : MonoBehaviour {
        public const string STATUS_WAITING_TO_CHOOSE = "Choose player to login";
        public const string STATUS_CONNECTING = "Connecting to server...";
        public const string STATUS_DOWNLOADING_GAME = "Connected to server -- downloading game data!";
        public const string STATUS_DOWNLOADING_PLAYER = "Connected to server -- downloading player data!";
        public const string STATUS_FAILED = "Failed to connect to server. Please close and try again.";

        public GameObject LoginFailurePopup;

        private ICrossBackend mBackend;

        private bool mBackendFailure = false;
        private bool mLoginProcessCanceled = false;

        private Login mLogin;   // is this the best way...?

        private AnalyticsTimer mLoginTimer = new AnalyticsTimer( LibraryAnalyticEvents.LOGIN_TIME, new MyTimer() );

        public GameObject PlayerSelectionArea;
        public TextMeshProUGUI LoginStatusText;

        [Inject]
        ITreasureDataManager TreasureDataManager;

        [Inject]
        IMonsterDataManager MonsterDataManager;

        [Inject]
        IPlayerDataManager PlayerDataManager;

        [Inject]
        IAppUpdateRequiredManager AppUpdateManager;

        [Inject]
        IUpcomingMaintenanceManager UpcomingMaintenanceManager;

        [Inject]
        IStringTableManager StringTableManager;

        [Inject]
        INewsManager NewsManager;

        [Inject]
        IBackendManager BackendManager;

        void Start() {
            mBackend = new CrossBackend();
            BackendManager.Init( mBackend );

            MyMessenger.Instance.AddListener( BackendMessages.LOGIN_SUCCESS, OnLoginSuccess );
            MyMessenger.Instance.AddListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );

            LoginStatusText.text = STATUS_WAITING_TO_CHOOSE;
        }

        public void OnLoginClicked() {
            Destroy( PlayerSelectionArea );
            LoginStatusText.text = STATUS_CONNECTING;

            mLogin = new Login( mBackend, mLoginTimer, string.Empty );
            mLogin.Start();
        }

        private void DoneLoadingData() {
            if ( !mBackendFailure ) {
                LoginStatusText.gameObject.SetActive( false );
                mLoginTimer.StopAndSendAnalytic();

                LoadMainScene();
            }
        }

        private void LoadMainScene() {
            SceneManager.LoadScene( "Main" );
        }

        void OnDestroy() {
            if ( mLogin != null ) {
                mLogin.OnDestroy();
            }
           
            MyMessenger.Instance.RemoveListener( BackendMessages.LOGIN_SUCCESS, OnLoginSuccess );
            MyMessenger.Instance.RemoveListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );
        }

        private void OnBackendFailure( IBackendFailure i_failure ) {
            if ( !mBackendFailure ) {
                mBackendFailure = true;
                //gameObject.InstantiateUI( LoginFailurePopup );    // right now this conflicts with OutOfSync popup
                LoginStatusText.text = STATUS_FAILED;
            }
        }

        private void OnLoginSuccess() {            
            StartCoroutine( ContinueLoginProcess() );
        }

        private IEnumerator ContinueLoginProcess() {
            yield return CheckToStopLoginProcess();

            if ( !mLoginProcessCanceled ) {
                yield return LoadDataFromBackend();
            }
        }

        private IEnumerator CheckToStopLoginProcess() {
            StringTableManager.Init( "English", mBackend );
            AppUpdateManager.Init( mBackend );
            while ( mBackend.IsBusy() ) {
                yield return 0;
            }

            if ( AppUpdateManager.IsUpgradeRequired() ) {
                AppUpdateManager.TriggerUpgradeViewIfRequired();
                mLoginProcessCanceled = true;
            }

            UpcomingMaintenanceManager.Init( mBackend );
            while ( mBackend.IsBusy() ) {
                yield return 0;
            }

            if ( UpcomingMaintenanceManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.During ) ) {
                UpcomingMaintenanceManager.TriggerUpcomingMaintenanceView();
                mLoginProcessCanceled = true;
            }           
        }

        private IEnumerator LoadDataFromBackend() {
            LoginStatusText.text = STATUS_DOWNLOADING_GAME;

            NewsManager.Init( mBackend );
            TreasureDataManager.Init( mBackend );
            MonsterDataManager.Init( mBackend );
            PlayerDataManager.Init( mBackend );

            //Constants.Init( mBackend );
            //GenericDataLoader.Init( mBackend );
            //GenericDataLoader.LoadDataOfClass<BuildingData>( GenericDataLoader.BUILDINGS );
            //GenericDataLoader.LoadDataOfClass<UnitData>( GenericDataLoader.UNITS );
            //GenericDataLoader.LoadDataOfClass<GuildData>( GenericDataLoader.GUILDS );

            while ( mBackend.IsBusy() ) {
                yield return 0;
            }
            mLoginTimer.StepComplete( LibraryAnalyticEvents.TITLE_DATA_TIME );

            //yield return SetUpPlayerData();
            
            DoneLoadingData();
        }

        private IEnumerator SetUpPlayerData() {
            LoginStatusText.text = STATUS_DOWNLOADING_PLAYER;

            // it's possible that the client is restarting and old player data exists -- we need to dispose of it
            /*if ( PlayerManager.Data != null ) {
                PlayerManager.Data.Dispose();
            }

            PlayerData playerData = new PlayerData();
            playerData.Init( mBackend );
            PlayerManager.Init( playerData );
            */
            while ( mBackend.IsBusy() ) {
                yield return 0;
            }
            /*
            playerData.AddDataStructures();
            playerData.CreateManagers();*/
            
            mLoginTimer.StepComplete( LibraryAnalyticEvents.INIT_PLAYER_TIME );
        }
    }
}