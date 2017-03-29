using Zenject;

namespace MyLibrary {
    public class LinkAccountView : GroupView {

        [Inject]
        ILinkAccountPM PM;

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
    }
}