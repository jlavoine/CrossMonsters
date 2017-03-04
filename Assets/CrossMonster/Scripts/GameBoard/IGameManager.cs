
namespace CrossMonsters {
    public interface IGameManager {
        bool IsGamePlaying();

        void Dispose();
        void OnAllMonstersDead();
    }
}
