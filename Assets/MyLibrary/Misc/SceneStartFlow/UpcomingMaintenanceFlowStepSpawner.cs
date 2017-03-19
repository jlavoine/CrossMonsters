
namespace MyLibrary {
    public interface IUpcomingMaintenanceFlowStepSpawner {
        IUpcomingMaintenanceFlowStep Create( ISceneStartFlowManager i_sceneManager );
    }

    public class UpcomingMaintenanceFlowStepSpawner : IUpcomingMaintenanceFlowStepSpawner {
        readonly UpcomingMaintenanceFlowStep.Factory factory;

        public UpcomingMaintenanceFlowStepSpawner( UpcomingMaintenanceFlowStep.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IUpcomingMaintenanceFlowStep Create( ISceneStartFlowManager i_manager ) {
            return factory.Create( i_manager );
        }
    }
}