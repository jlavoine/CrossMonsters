using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class AllTreasureView : GroupView {
        [Inject]
        AllTreasurePM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        public void ShowView() {
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }
    }
}