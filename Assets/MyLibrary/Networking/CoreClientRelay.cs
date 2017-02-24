using UnityEngine.Networking;
using MyLibrary;
using UnityEngine.Networking.NetworkSystem;
using Newtonsoft.Json;

namespace MyLibrary {
    public abstract class CoreClientRelay {
        protected IUnityNetworkWrapper mNetwork;

        public CoreClientRelay() {}

        public void SetNetworkClient( IUnityNetworkWrapper i_network ) {
            mNetwork = i_network;
        }

        /// <summary>
        /// This method registers with the network i_messageType, shared both by client and server.
        /// When it receives this message, it deserializes the response into T1, then broadcasts a message
        /// with i_messageToBroadcast and type T2.
        /// T2 is generally an interface that T1 implements, allowing for the receiving methods to be testable.
        /// </summary>
        public void RegisterMessageHandler<T1, T2>( short i_messageType, string i_messageToBroadcast ) where T1 : T2 {
            mNetwork.RegisterMessageHandler( i_messageType, ( message ) => {
                StringMessage messageFromServer = message.ReadMessage<StringMessage>();
                T1 data = JsonConvert.DeserializeObject<T1>( messageFromServer.value );
                MyMessenger.Instance.Send<T2>( i_messageToBroadcast, data );
            } );
        }
    }
}