using System;

namespace MyLibrary {
    public interface IUpcomingMaintenanceManager {
        void Init( IBasicBackend i_backend );
        void TriggerUpcomingMaintenanceView();

        DateTime GetMaintenanceStartTime();
        DateTime GetMaintenanceEndTime();

        bool ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels i_concern );
        bool IsAnyUpcomingMaintenance();
        bool IsWithinWarningTime();
        bool IsDuringMaintenance();
        bool HasUserBeenWarned { get; }
    }
}
