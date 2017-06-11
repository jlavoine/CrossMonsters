
namespace MonsterMatch {
    public class CurrentGauntletManager : ICurrentGauntletManager {

        private bool mGauntletSessionInProgress = false;
        public bool IsGauntletSessionInProgress { get { return mGauntletSessionInProgress; } set { mGauntletSessionInProgress = value; } }

        private bool mComingFromGauntletVictory = false;
        public bool ComingFromGauntletVictory { get { return mComingFromGauntletVictory; } set { mComingFromGauntletVictory = value; } }
    }
}