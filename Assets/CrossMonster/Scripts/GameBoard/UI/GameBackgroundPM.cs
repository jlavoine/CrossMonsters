using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class GameBackgroundPM : PresentationModel {
        [Inject]
        IChainBuilder ChainManager;

        public GameBackgroundPM() {}

        public void PlayerEnteredBackground() {
            ChainManager.CancelChain();
        }
    }
}
