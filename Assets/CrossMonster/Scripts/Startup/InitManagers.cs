using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    public class InitManagers : MonoBehaviour {

        [Inject]
        MyLogger Logger;

        void Awake() {
            DontDestroyOnLoad( this );

            //MyLogger logger = new MyLogger();
            //UnityAnalyticsManager analytics = new UnityAnalyticsManager( new MyUnityAnalytics() );
            InfoPopupManager popupManager = new InfoPopupManager();
            OutOfSyncManager outOfSyncManager = new OutOfSyncManager();
        }
    }
}
