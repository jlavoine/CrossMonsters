using Zenject;

namespace MyLibrary {
    public class ShowNewsFlowStep : BaseSceneStartFlowStep, IShowNewsFlowStep {
        readonly IMessageService mMessenger;
        readonly INewsManager mManager;
        readonly IAllNewsPM mNewsPM;

        public ShowNewsFlowStep( INewsManager i_manager, IMessageService i_messenger, IAllNewsPM i_newsPM, ISceneStartFlowManager i_sceneManager ) : base (i_sceneManager ) {
            mMessenger = i_messenger;
            mManager = i_manager;
            mNewsPM = i_newsPM;            
        }

        public override void Start() {
            ListenForMessages( true );

            if ( mManager.ShouldShowNews() ) {
                mNewsPM.Show();
                mManager.UpdateLastSeenNewsTime();
            } else {
                Done();
            }
        }

        protected override void OnDone() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                mMessenger.AddListener( AllNewsPM.NEWS_DIMISSED_EVENT, Done );
            } else {
                mMessenger.RemoveListener( AllNewsPM.NEWS_DIMISSED_EVENT, Done );
            }
        }

        public class Factory : Factory<ISceneStartFlowManager, ShowNewsFlowStep> { }
    }
}