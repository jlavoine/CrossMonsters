using MyLibrary;

namespace CrossMonsters {
    public class PlayerDataManager : IPlayerDataManager {
        public const string GOLD_KEY = "G1";

        private IBasicBackend mBackend;

        private int mGold;
        public int Gold { get { return mGold; } set { mGold = value; } }

        public void Init( IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing player data manager" );
            mBackend = i_backend;

            DownloadPlayerGold();
        }

        private void DownloadPlayerGold() {
            mBackend.GetVirtualCurrency( GOLD_KEY, ( result ) => {
                Gold = result;
            } );
        }
    }
}
