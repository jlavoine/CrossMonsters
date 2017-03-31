using Zenject;

namespace MyLibrary {
    public class RemoveDeviceLinkView : GroupView {
        [Inject]
        IRemoveDeviceLinkPM PM;

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

        public void OnConfirmClicked() {
            PM.RemoveDeviceFromAccount();
        }
    }
}
