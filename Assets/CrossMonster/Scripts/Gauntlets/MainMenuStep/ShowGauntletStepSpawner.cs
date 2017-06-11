using MyLibrary;

namespace MonsterMatch {
    public interface IShowGauntletStepSpawner {
        IShowGauntletStep Create( ISceneStartFlowManager i_sceneManager );
    }

    public class ShowGauntletStepSpawner : IShowGauntletStepSpawner {
        readonly ShowGauntletStep.Factory factory;

        public ShowGauntletStepSpawner( ShowGauntletStep.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IShowGauntletStep Create( ISceneStartFlowManager i_manager ) {
            return factory.Create( i_manager );
        }
    }
}