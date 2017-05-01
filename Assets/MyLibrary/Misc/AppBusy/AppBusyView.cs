using Zenject;

namespace MyLibrary {
    public class AppBusyView : GroupView {
        [Inject]
        IAppBusyPM PM;

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
