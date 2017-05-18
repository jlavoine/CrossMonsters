using MyLibrary;

namespace MonsterMatch {
    public interface IShowLoginPromosStepSpawner {
        IShowLoginPromosStep Create( ISceneStartFlowManager i_sceneManager );
    }

    public class ShowLoginPromosStepSpawner : IShowLoginPromosStepSpawner {
        readonly ShowLoginPromosStep.Factory factory;

        public ShowLoginPromosStepSpawner( ShowLoginPromosStep.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IShowLoginPromosStep Create( ISceneStartFlowManager i_manager ) {
            return factory.Create( i_manager );
        }
    }
}