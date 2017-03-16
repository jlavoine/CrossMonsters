
namespace MyLibrary {
    public class UpcomingMaintenanceData : IUpcomingMaintenanceData {
        public bool IsUpcomingMaintenance;
        public double StartSecondsFromEpoch;
        public double EndSecondsFromEpoch;
        public int WarningTimeInMinutes;

        public bool IsAnyUpcomingMaintenance() {
            return IsUpcomingMaintenance;
        }

        public double GetStartSecondsFromEpoch() {
            return StartSecondsFromEpoch;
        }

        public double GetEndSecondsFromEpoch() {
            return EndSecondsFromEpoch;
        }

        public int GetWarningTimeInMinutes() {
            return WarningTimeInMinutes;
        }
    }
}
