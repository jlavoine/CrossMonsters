
namespace MonsterMatch {
    public interface IGameManager {
        bool IsGamePlaying();

        void Dispose();
        void OnAllMonstersDead();
        void PrepareForNextWave();
        void BeginWaveGameplay();
        void SetState( GameStates i_state );
    }
}
