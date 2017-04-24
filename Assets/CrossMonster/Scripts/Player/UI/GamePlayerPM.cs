using MyLibrary;

namespace MonsterMatch {
    public class GamePlayerPM : PresentationModel, IGamePlayerPM {
        public const string HP_PROPERTY = "RemainingHealth";

        private IGamePlayer mPlayer;

        public GamePlayerPM( IGamePlayer i_player ) {
            mPlayer = i_player;
            ListenForMessages( true );
            UpdateRemainingHealthProperty();
        }

        protected override void _Dispose() {
            mPlayer.Dispose();
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( GameMessages.UPDATE_PLAYER_HP, UpdateRemainingHealthProperty );
            } else {
                MyMessenger.Instance.RemoveListener( GameMessages.UPDATE_PLAYER_HP, UpdateRemainingHealthProperty );
            }
        }

        private void UpdateRemainingHealthProperty() {
            ViewModel.SetProperty( HP_PROPERTY, mPlayer.HP );
        }
    }
}