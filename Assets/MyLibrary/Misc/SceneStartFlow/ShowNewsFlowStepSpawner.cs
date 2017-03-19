
namespace MyLibrary {
    public interface IShowNewsStepSpawner {
        IShowNewsFlowStep Create( ISceneStartFlowManager i_sceneManager );
    }

    public class ShowNewsStepSpawner : IShowNewsStepSpawner {
        readonly ShowNewsFlowStep.Factory factory;

        public ShowNewsStepSpawner( ShowNewsFlowStep.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IShowNewsFlowStep Create( ISceneStartFlowManager i_manager ) {
            return factory.Create( i_manager );
        }
    }
}