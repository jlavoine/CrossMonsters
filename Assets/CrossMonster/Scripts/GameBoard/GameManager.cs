
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
        }

        public void Dispose() {

        }

        private void SetState( GameStates i_state ) {
            State = i_state;
        }
    }
}
