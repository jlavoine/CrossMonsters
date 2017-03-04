using MyLibrary;

namespace CrossMonsters {
    public class GameOverPM : PresentationModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string BODY_TEXT_PROPERTY = "GameOverBody";

        public const string LOST_GAME_KEY = "GameOver_Lost";
        public const string WON_GAME_KEY = "GameOver_Won";

        public GameOverPM() {
            SetVisibleProperty( false );
            ListenForMessages( true );
        }

        protected override void _Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<bool>( GameMessages.GAME_OVER, OnGameOver );
            }
            else {
                MyMessenger.Instance.RemoveListener<bool>( GameMessages.GAME_OVER, OnGameOver );
            }
        }

        public void OnGameOver( bool i_win ) {
            SetVisibleProperty( true );
            SetBodyTextProperty( i_win );
        }

        private void SetBodyTextProperty( bool i_won ) {
            string text = StringTableManager.Instance.Get( i_won ? WON_GAME_KEY : LOST_GAME_KEY );
            ViewModel.SetProperty( BODY_TEXT_PROPERTY, text );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
