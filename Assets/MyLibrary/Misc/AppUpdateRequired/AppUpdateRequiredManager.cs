﻿
namespace MyLibrary {
    public class AppUpdateRequirdManager : IAppUpdateRequiredManager {
        public const string LATEST_VERSION_TITLE_KEY = "AppVersion";

        public const float CURRENT_VERSION = 0.10f; // TODO update manually with each release

        readonly IAppUpdateRequiredPM mAppUpgradeRequiredPM;

        private IBasicBackend mBackend;
        private float mLatestVersion;

        public float LatestAppVersion { get { return mLatestVersion; } set { mLatestVersion = value; }  }

        public AppUpdateRequirdManager( IAppUpdateRequiredPM i_pm ) {
            mAppUpgradeRequiredPM = i_pm;
        }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadLatestAppVersion();
        }

        public bool IsUpgradeRequired() {
            return CURRENT_VERSION < mLatestVersion;
        }

        public void TriggerUpgradeViewIfRequired() {
            if ( IsUpgradeRequired() ) {
                mAppUpgradeRequiredPM.Show();
            }
        }

        private void DownloadLatestAppVersion() {
            mBackend.GetTitleData( LATEST_VERSION_TITLE_KEY, ( result ) => {
                LatestAppVersion = float.Parse( result );
            } );
        }
    }
}