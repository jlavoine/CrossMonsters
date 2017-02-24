using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace MyLibrary {
    public class UnityNetworkWrapper : IUnityNetworkWrapper {
        private NetworkClient mClient;
        public NetworkClient Client { get { return mClient; } }

        public UnityNetworkWrapper( NetworkClient i_client ) {
            mClient = i_client;
        }

        public void RegisterMessageHandler( short i_messageType, NetworkMessageDelegate i_callback ) {
            Client.RegisterHandler( i_messageType, i_callback );
        }

        public void SendMessage( short i_messageType, StringMessage i_message ) {
            Client.Send( i_messageType, i_message );
        }
    }
}
