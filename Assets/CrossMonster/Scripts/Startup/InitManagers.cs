using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    public class InitManagers : MonoBehaviour {

        void Awake() {
            DontDestroyOnLoad( this );

            MyLogger logger = new MyLogger();
            //UnityAnalyticsManager analytics = new UnityAnalyticsManager( new MyUnityAnalytics() );
            InfoPopupManager popupManager = new InfoPopupManager();
            OutOfSyncManager outOfSyncManager = new OutOfSyncManager();
        }
    }
}
