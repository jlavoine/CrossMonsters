using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace MyLibrary {
    public interface IUnityNetworkWrapper {
        NetworkClient Client { get; }

        void RegisterMessageHandler( short i_messageType, NetworkMessageDelegate i_callback );
        void SendMessage( short i_messageType, StringMessage i_message );
    }
}