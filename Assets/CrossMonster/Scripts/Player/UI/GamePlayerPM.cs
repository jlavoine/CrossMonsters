using MyLibrary;

namespace CrossMonsters {
    public class GamePlayerPM : PresentationModel {
        public const string HP_PROPERTY = "RemainingHealth";

        private IGamePlayer mPlayer;

        public GamePlayerPM( IGamePlayer i_player ) {
            mPlayer = i_player;

            UpdateRemainingHealthProperty();
        }        

        private void UpdateRemainingHealthProperty() {
            ViewModel.SetProperty( HP_PROPERTY, mPlayer.HP );
        }
    }
}