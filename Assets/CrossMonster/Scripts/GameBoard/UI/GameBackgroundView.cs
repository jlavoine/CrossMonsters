using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GameBackgroundView : GroupView {
        [Inject]
        IChainBuilder ChainManager;

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
