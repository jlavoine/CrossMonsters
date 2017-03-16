
namespace MyLibrary {
    public interface IUpcomingMaintenanceData {
        bool IsAnyUpcomingMaintenance();
        double GetStartSecondsFromEpoch();
        double GetEndSecondsFromEpoch();
        int GetWarningTimeInMinutes();        
    }
}