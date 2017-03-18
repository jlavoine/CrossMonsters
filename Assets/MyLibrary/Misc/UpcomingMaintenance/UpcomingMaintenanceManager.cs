using Newtonsoft.Json;
using System;

namespace MyLibrary {
    public enum MaintenanceConcernLevels {
        Close,
        During
    }

    public class UpcomingMaintenanceManager : IUpcomingMaintenanceManager {
        public const string UPCOMING_MAINTENANCE_TITLE_KEY = "UpcomingMaintenance";
        public const string TRIGGER_MAINTENANCE_POPUP = "TriggerUpcomingMaintenancePopup";

        readonly IMessageService Messenger;

        private IBasicBackend mBackend;

        private IUpcomingMaintenanceData mData;
        public IUpcomingMaintenanceData Data { get { return mData; } set { mData = value; } }

        private bool mUserWarned = false;
        public bool HasUserBeenWarned { get { return mUserWarned; } set { mUserWarned = value; } }

        public UpcomingMaintenanceManager( IMessageService i_messenger ) {
            Messenger = i_messenger;
        }

        public void Init( IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing upcoming maintenance manager" );
            mBackend = i_backend;

            DownloadUpcomingMaintenanceData();
        }

        public bool IsAnyUpcomingMaintenance() {
            return mData.IsAnyUpcomingMaintenance();
        }

        public bool IsWithinWarningTime() {
            DateTime beginMaintenanceTime = GetMaintenanceStartTime();
            TimeSpan timeDifference = beginMaintenanceTime - mBackend.GetDateTime();

            return timeDifference.TotalMinutes <= Data.GetWarningTimeInMinutes();
        }

        public bool IsDuringMaintenance() {
            DateTime beginMaintenanceTime = GetMaintenanceStartTime();
            TimeSpan timeDifference = beginMaintenanceTime - mBackend.GetDateTime();

            return timeDifference.TotalMilliseconds <= 0;
        }        

        public bool ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels i_concern ) {
            if ( !IsAnyUpcomingMaintenance() ) {
                return false;
            }

            if ( i_concern == MaintenanceConcernLevels.Close ) {
                if ( IsWithinWarningTime() && !HasUserBeenWarned ) {
                    return true;
                }
            }

            bool shouldTrigger = IsDuringMaintenance();
            return shouldTrigger;
        }

        public void TriggerUpcomingMaintenanceView() {
            HasUserBeenWarned = true;
            bool canDismiss = !IsDuringMaintenance();
            Messenger.Send<bool>( TRIGGER_MAINTENANCE_POPUP, canDismiss );
        }

        public DateTime GetMaintenanceStartTime() {
            DateTime beginMaintenanceTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            beginMaintenanceTime = beginMaintenanceTime.AddSeconds( Data.GetStartSecondsFromEpoch() );

            return beginMaintenanceTime;
        }

        public DateTime GetMaintenanceEndTime() {
            DateTime endTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            endTime = endTime.AddSeconds( Data.GetEndSecondsFromEpoch() );

            return endTime;
        }

        private void DownloadUpcomingMaintenanceData() {
            mBackend.GetTitleData( UPCOMING_MAINTENANCE_TITLE_KEY, ( result ) => {
                Data = JsonConvert.DeserializeObject<UpcomingMaintenanceData>( result );                
            } );
        }
    }
}
