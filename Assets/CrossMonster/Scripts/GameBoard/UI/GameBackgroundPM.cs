using MyLibrary;

namespace CrossMonsters {
    public class GameBackgroundPM : PresentationModel {

        public GameBackgroundPM() {}

        public void PlayerEnteredBackground() {
            ChainManager.Instance.CancelChain();
        }
    }
}
