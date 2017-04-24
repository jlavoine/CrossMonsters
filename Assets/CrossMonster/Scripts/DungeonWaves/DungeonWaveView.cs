using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class DungeonWaveView : GroupView {

        [Inject]
        IDungeonWavePM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        public void OnReadyClicked() {
            PM.Hide();
        }
    }
}
