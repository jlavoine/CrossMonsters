using MyLibrary;
using Zenject;

namespace CrossMonsters {
    public class GameManager : IGameManager, IInitializable {
        [Inject]
        IMessageService MyMessenger;

        private GameStates mState;
        public GameStates State { get { return mState; } set { mState = value; } }

        public GameManager() {
            SetState( GameStates.Playing );
        }

        public void Initialize() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.AddListener( GameMessages.PLAYER_DEAD, OnPlayerDied );
            } else {
                MyMessenger.RemoveListener( GameMessages.PLAYER_DEAD, OnPlayerDied );
            }
        }

        public void OnPlayerDied() {
            SetState( GameStates.Ended );
            SendGameOverMessage( false );
        }

        public void OnAllMonstersDead() {
            SetState( GameStates.Ended );
            SendGameOverMessage( true );
        }

        public bool IsGamePlaying() {
            return State == GameStates.Playing;
        }

        private void SetState( GameStates i_state ) {
            State = i_state;
        }

        private void SendGameOverMessage( bool i_won ) {
            MyMessenger.Send<bool>( GameMessages.GAME_OVER, i_won );
        }
    }
}
