using System;
using Zenject;

namespace MyLibrary {
    public class UpcomingMaintenanceFlowStep : BaseSceneStartFlowStep, IUpcomingMaintenanceFlowStep {
        readonly IMessageService mMessenger;
        readonly IUpcomingMaintenanceManager mManager;

        public UpcomingMaintenanceFlowStep( IUpcomingMaintenanceManager i_manager, IMessageService i_messenger, ISceneStartFlowManager i_sceneManager ) :
            base (i_sceneManager ) {
            mManager = i_manager;
            mMessenger = i_messenger;

            ListenForMessages( true );
        }

        public override void Start() {
            if ( mManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ) ) {
                mManager.TriggerUpcomingMaintenanceView();
            } else {
                Done();
            }
        }

        protected override void OnDone() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                mMessenger.AddListener( UpcomingMaintenancePM.DISMISSED_EVENT, Done );
            }
            else {
                mMessenger.RemoveListener( UpcomingMaintenancePM.DISMISSED_EVENT, Done );
            }
        }

        public class Factory : Factory<ISceneStartFlowManager, UpcomingMaintenanceFlowStep> { }
    }
}
