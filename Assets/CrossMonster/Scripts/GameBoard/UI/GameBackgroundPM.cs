using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GameBackgroundPM : PresentationModel {
        [Inject]
        IChainBuilder ChainManager;

        public GameBackgroundPM() {}

        public void PlayerEnteredBackground() {
            ChainManager.CancelChain();
        }
    }
}
