using System.Collections.Generic;
using MyLibrary;
using Zenject;
using Newtonsoft.Json;

/// <summary>
/// DO NOT USE. I was starting to do this because I thought it was correct,
/// but it turned out to be kind of verbose.
/// </summary>

namespace MonsterMatch {
    public interface IOpenTimedChestRequest {
        void Send( Callback<IOpenTimedChestResponse> i_callback );
    }

    public interface IOpenTimedChestRequest_Spawner {
        IOpenTimedChestRequest Create( string i_chestId );
    }

    public class OpenTimedChestRequest_Spawner : IOpenTimedChestRequest_Spawner {
        readonly OpenTimedChestRequest.Factory factory;

        public OpenTimedChestRequest_Spawner( OpenTimedChestRequest.Factory i_factory ) {
            this.factory = i_factory;
        }

        public IOpenTimedChestRequest Create( string i_chestId ) {
            return factory.Create( i_chestId );
        }
    }

    public class OpenTimedChestRequest : IOpenTimedChestRequest {
        public const string CHEST_ID = "ChestId";

        readonly IBackendManager mBackend;

        private Dictionary<string, string> mParams = new Dictionary<string, string>();

        public OpenTimedChestRequest( IBackendManager i_backend, string i_chestId ) {
            mBackend = i_backend;
            mParams.Add( CHEST_ID, i_chestId );
        }

        public void Send( Callback<IOpenTimedChestResponse> i_callback ) {
            mBackend.MakeCloudCall( BackendMethods.OPEN_TIMED_CHEST, mParams, (result) => {
                i_callback( JsonConvert.DeserializeObject<OpenTimedChestResponse>( result["data"] ) );
            } );
        }

        public class Factory : Factory<string, OpenTimedChestRequest> { }
    }
}
