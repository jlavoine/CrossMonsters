using UnityEngine;

namespace MyLibrary {
    public class SendMessageFromAnimation : MonoBehaviour {
        public string Message;

        public void SendAnimationMessage() {
            MyMessenger.Instance.Send( Message );
        }
    }
}
