using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class GameManager : IGameManager, IInitializable {
        [Inject]
        IMessageService MyMessenger;

        [Inject]
        IBackendManager BackendManager;

        [Inject]
        ICurrentDungeonGameManager CurrentDungeonManager;

        [Inject]
        IDungeonWavePM DungeonWavePM;

        [Inject]
        IAudioManager Audio;

        [Inject]
        IGamePlayer Player;

        private GameStates mState = GameStates.Paused;
        public GameStates State { get { return mState; } set { mState = value; } }

        public GameManager() {            
        }

        public void Initialize() {
            ListenForMessages( true );
            PrepareForNextWave();
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        public void PrepareForNextWave() {
            SetState( GameStates.Paused );
            DungeonWavePM.Show();
            Player.OnWaveFinished();
        }

        public void BeginWaveGameplay() {
            SetState( GameStates.Playing );
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
            Audio.PlayOneShot( CombatAudioKeys.GAME_OVER_LOSS );
        }

        public void OnAllMonstersDead() {
            SetState( GameStates.Ended );
            SendGameOverMessage( true );
            AwardDungeonRewards();
            Audio.PlayOneShot( CombatAudioKeys.GAME_OVER_WIN );
        }

        public bool IsGamePlaying() {
            return State == GameStates.Playing;
        }

        public void SetState( GameStates i_state ) {
            State = i_state;
        }

        private void SendGameOverMessage( bool i_won ) {
            MyMessenger.Send<bool>( GameMessages.GAME_OVER, i_won );
        }

        private void AwardDungeonRewards() {
            CurrentDungeonManager.AwardRewards();
            BackendManager.MakeCloudCall( BackendMethods.COMPLETE_DUNGEON_SESSION, null, null );
        }
    }
}
