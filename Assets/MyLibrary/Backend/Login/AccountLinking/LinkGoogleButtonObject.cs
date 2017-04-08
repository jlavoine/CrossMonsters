using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class LinkGoogleButtonObject : MonoBehaviour {

        [Inject]
        ILinkGoogleButton Button;

        public void OnClick() {
            Button.OnClick();
        }
    }
}