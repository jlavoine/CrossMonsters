using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GameBackgroundPM : PresentationModel {
        [Inject]
        IChainManager ChainManager;

        public GameBackgroundPM() {}

        public void PlayerEnteredBackground() {
            ChainManager.CancelChain();
        }
    }
}
