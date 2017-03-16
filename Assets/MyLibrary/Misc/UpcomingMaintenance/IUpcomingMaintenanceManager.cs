using System;

namespace MyLibrary {
    public interface IUpcomingMaintenanceManager {
        void Init( IBasicBackend i_backend );
        void TriggerUpcomingMaintenanceView( bool i_canDismissPopup );

        DateTime GetMaintenanceStartTime();
        DateTime GetMaintenanceEndTime();

        bool IsAnyUpcomingMaintenance();
        bool IsWithinWarningTime( DateTime i_time );
        bool IsDuringMaintenance( DateTime i_time );
        bool HasUserBeenWarned { get; }
    }
}
