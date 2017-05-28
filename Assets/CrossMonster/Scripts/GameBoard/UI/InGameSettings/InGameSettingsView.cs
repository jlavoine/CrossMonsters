using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class InGameSettingsView : GroupView {

        [Inject]
        IInGameSettingsPM PM;

        [Inject]
        IGameManager GameManager;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        public void ShowView() {
            if ( GameManager.IsGamePlaying() ) {
                PM.Show();
            }
        }

        public void HideView() {
            PM.Hide();
        }
    }
}
