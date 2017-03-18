using MyLibrary;

namespace CrossMonsters {
    public class MainMenuFlow {
        readonly IUpcomingMaintenanceManager mUpcomingMaintenance;

        public MainMenuFlow( IUpcomingMaintenanceManager i_upcomingMaintenance ) {
            mUpcomingMaintenance = i_upcomingMaintenance;
        }

        public void Start() {
            CheckForUpcomingMaintenance();
        }

        private void CheckForUpcomingMaintenance() {
            if ( mUpcomingMaintenance.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ) ) {
                mUpcomingMaintenance.TriggerUpcomingMaintenanceView();
            }
        }
    }
}