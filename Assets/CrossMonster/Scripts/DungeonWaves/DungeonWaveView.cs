using MyLibrary;
using Zenject;

namespace CrossMonsters {
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
