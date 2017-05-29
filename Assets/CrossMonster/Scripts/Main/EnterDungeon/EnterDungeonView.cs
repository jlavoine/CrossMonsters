using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class EnterDungeonView : GroupView {

        [Inject]
        IEnterDungeonPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowView() {
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }

        public void LoadDungeonClicked() {
            PM.LoadDungeon();
        }
    }
}