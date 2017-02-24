using UnityEngine.Networking;

namespace MyLibrary {
    public class AuthTicketMessage : MessageBase {
        public string PlayFabId;
        public string AuthTicket;
        public bool IsLocal;
    }
}