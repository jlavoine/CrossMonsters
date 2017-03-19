
namespace MyLibrary {
    public abstract class BaseSceneStartFlowStep : ISceneStartFlowStep {
        public abstract void Start();
        protected abstract void OnDone();

        private ISceneStartFlowManager mManager;

        public BaseSceneStartFlowStep( ISceneStartFlowManager i_flowManager ) {
            mManager = i_flowManager;
        }

        public void Done() {
            mManager.StepFinished();
            OnDone();
        }
    }
}
