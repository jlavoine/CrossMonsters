using MyLibrary;
using System;
using Newtonsoft.Json;
using Zenject;

namespace MonsterMatch {
    public class PlayerDataManager : IPlayerDataManager {
        public const string GOLD_KEY = "G1";
        public const string PLAYER_STATS_KEY = "StatsProgress";
        public const string STATS_INFO_KEY = "StatInfo";

        private IBasicBackend mBackend;

        [Inject]
        IMessageService Messenger;

        private int mGold;
        public int Gold { get { return mGold; }
            set {
                mGold = value;
                Messenger.Send( GameMessages.PLAYER_GOLD_CHANGED );
            }
        }

        private IPlayerStatData mStatData;
        public IPlayerStatData PlayerStatData { get { return mStatData; } set { mStatData = value; } }

        private IStatInfoData mStatInfoData;
        public IStatInfoData StatInfoData { get { return mStatInfoData; } set { mStatInfoData = value; } }

        public void Init( IBasicBackend i_backend ) {   
            mBackend = i_backend;

            DownloadPlayerGold();
            DownloadPlayerStatData();
            DownloadPlayerStatProgression();
        }

        public int GetStat( string i_key ) {
            int stat = (int)Math.Ceiling( mStatData.GetStatLevel( i_key ) * mStatInfoData.GetValuePerLevel( i_key ) );
            return stat;
        }

        private void DownloadPlayerGold() {
            mBackend.GetVirtualCurrency( GOLD_KEY, ( result ) => {
                mGold = result; // not use the property on purpose so it doesn't trigger a message
            } );
        }

        private void DownloadPlayerStatData() {
            mBackend.GetReadOnlyPlayerData( PLAYER_STATS_KEY, ( result ) => {
                mStatData = JsonConvert.DeserializeObject<PlayerStatData>( result );
            } );
        }

        private void DownloadPlayerStatProgression() {
            mBackend.GetTitleData( STATS_INFO_KEY, ( result ) => {
                mStatInfoData = JsonConvert.DeserializeObject<StatInfoData>( result );
            } );
        }
    }
}
