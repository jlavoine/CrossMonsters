using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class PlayerDataManager : IPlayerDataManager {
        public const string GOLD_KEY = "G1";

        private IBasicBackend mBackend;

        private int mGold;
        public int Gold { get { return mGold; } set { mGold = value; } }

        private Dictionary<string, int> mStatData;

        private Dictionary<string, int> mStatProgression;

        public void Init( IBasicBackend i_backend ) {            
            mBackend = i_backend;

            DownloadPlayerGold();
            DownloadPlayerStatData();
            DownloadPlayerStatProgression();
        }

        private void DownloadPlayerGold() {
            mBackend.GetVirtualCurrency( GOLD_KEY, ( result ) => {
                Gold = result;
            } );
        }

        private void DownloadPlayerStatData() {

        }

        private void DownloadPlayerStatProgression() {

        }
    }
}
