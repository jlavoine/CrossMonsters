using MyLibrary;

namespace MonsterMatch {
    public class PlayerSummaryPM : PresentationModel, IPlayerSummaryPM {
        public const string GOLD_PROPERTY = "Gold";

        readonly IPlayerDataManager mManager;
        readonly IMessageService mMessenger;

        public PlayerSummaryPM( IPlayerDataManager i_manager, IMessageService i_messenger ) {
            mMessenger = i_messenger;
            mManager = i_manager;

            ListenForMessages( true );

            SetGoldProperty();
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
            ViewModel.SetProperty( GOLD_PROPERTY, mManager.Gold.ToString() );
        }
    }
}
