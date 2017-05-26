using MyLibrary;

namespace MonsterMatch {
    public class PlayerSummaryPM : PresentationModel, IPlayerSummaryPM {
        public const string GOLD_PROPERTY = "Gold";
        public const string TREASURE_LEVEL_PROPERTY = "TreasureLevel";

        readonly IPlayerDataManager mPlayerManager;
        readonly IMessageService mMessenger;
        readonly ITreasureDataManager mTreasureManager;

        public PlayerSummaryPM( IPlayerDataManager i_playerManager, ITreasureDataManager i_treasureManager, IMessageService i_messenger ) {
            mMessenger = i_messenger;
            mPlayerManager = i_playerManager;
            mTreasureManager = i_treasureManager;

            ListenForMessages( true );

            SetGoldProperty();
            SetTreasureLevelProperty();
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if (i_listen) {
                mMessenger.AddListener( GameMessages.PLAYER_GOLD_CHANGED, SetGoldProperty );
            } else {
                mMessenger.RemoveListener( GameMessages.PLAYER_GOLD_CHANGED, SetGoldProperty );
            }
        }

        private void SetGoldProperty() {
            ViewModel.SetProperty( GOLD_PROPERTY, mPlayerManager.Gold.ToString() );
        }

        private void SetTreasureLevelProperty() {
            ViewModel.SetProperty( TREASURE_LEVEL_PROPERTY, mTreasureManager.GetPlayerTreasureLevel().ToString() );
        }
    }
}
