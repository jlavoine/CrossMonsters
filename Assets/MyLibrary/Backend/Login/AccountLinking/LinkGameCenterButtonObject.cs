using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class LinkGameCenterButtonObject : MonoBehaviour {

        [Inject]
        ILinkGameCenterButton Button;

        public void OnClick() {
            Button.OnClick();
        }
    }
}