using MyLibrary;

namespace CrossMonsters {
    public class GameManager : IGameManager {
        private static IGameManager mInstance;
        public static IGameManager Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new GameManager();
                }
                return mInstance;
            }
            set {
                // tests only!
                mInstance = value;
            }
        }

        private GameStates mState;
        public GameStates State { get { return mState; } set { mState = value; } }

        public GameManager() {
            SetState( GameStates.Playing );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( GameMessages.PLAYER_DEAD, OnPlayerDied );
            } else {
                MyMessenger.Instance.RemoveListener( GameMessages.PLAYER_DEAD, OnPlayerDied );
            }
        }

        public void OnPlayerDied() {
            SetState( GameStates.Ended );
        }

        public bool IsGamePlaying() {
            return State == GameStates.Playing;
        }

        private void SetState( GameStates i_state ) {
            State = i_state;
        }
    }
}
