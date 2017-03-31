using Zenject;

namespace MyLibrary {
    public class LinkAccountPM : BasicWindowPM, ILinkAccountPM {

        [Inject]
        IRemoveDeviceLinkPM RemoveDevicePM;

        public LinkAccountPM() {
            SetVisibleProperty( false );
        }

        public void ShowRemoveDeviceFromAccountPopup() {
            RemoveDevicePM.Show();
        }
    }
}
