using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GameBackgroundView : GroupView {
        [Inject]
        IChainManager ChainManager;

        [Inject]
        GameBackgroundPM BackgroundPM;

        void Start() {
            SetModel( BackgroundPM.ViewModel );
        }

        public void OnPointerEnter() {
            if ( ChainManager.IsActiveChain() ) {
                BackgroundPM.PlayerEnteredBackground();
            }
        }
    }
}
