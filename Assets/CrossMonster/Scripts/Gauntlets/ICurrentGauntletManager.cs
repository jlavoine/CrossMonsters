
namespace MonsterMatch {
    public interface ICurrentGauntletManager {
        int CurrentGauntletIndex { get; set; }

        bool ComingFromGauntletVictory { get; set; }
        bool IsGauntletSessionInProgress();
    }
}
