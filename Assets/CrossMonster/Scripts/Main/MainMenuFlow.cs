using MyLibrary;

namespace CrossMonsters {
    public class MainMenuFlow {
        readonly IUpcomingMaintenanceManager mUpcomingMaintenance;
        readonly INewsManager mNewsManager;
        readonly IAllNewsPM mNewsPM;

        public MainMenuFlow( IUpcomingMaintenanceManager i_upcomingMaintenance, INewsManager i_newsManager, IAllNewsPM i_newsPM ) {
            mUpcomingMaintenance = i_upcomingMaintenance;
            mNewsManager = i_newsManager;
            mNewsPM = i_newsPM;
        }

        public void Start() {
            CheckForUpcomingMaintenance();
            CheckForUnseenNews();
        }

        private void CheckForUpcomingMaintenance() {
            if ( mUpcomingMaintenance.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ) ) {
                mUpcomingMaintenance.TriggerUpcomingMaintenanceView();
            }
        }

        private void CheckForUnseenNews() {
            if ( mNewsManager.ShouldShowNews() ) {
                mNewsPM.Show();
                mNewsManager.UpdateLastSeenNewsTime();
            }
        }
    }
}