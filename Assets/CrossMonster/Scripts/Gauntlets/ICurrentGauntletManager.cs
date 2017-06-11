
namespace MonsterMatch {
    public interface ICurrentGauntletManager {
        bool ComingFromGauntletVictory { get; set; }
        bool IsGauntletSessionInProgress { get; set; }
    }
}
