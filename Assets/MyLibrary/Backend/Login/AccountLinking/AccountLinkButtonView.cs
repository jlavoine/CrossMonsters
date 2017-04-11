using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class AccountLinkButtonView : MonoBehaviour {
        public LoginMethods LinkType;

        [Inject]
        ILinkAccountButton Button;

        public void OnClick() {
            Button.OnClick( LinkType );
        }
    }
}