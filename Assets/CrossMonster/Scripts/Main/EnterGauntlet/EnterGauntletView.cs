using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class EnterGauntletView : GroupView {
        public int Index;

        [Inject]
        IEnterGauntletPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowView() {
            PM.SetIndex( Index );
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }
    }
}