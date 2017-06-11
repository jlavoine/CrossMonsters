
namespace MonsterMatch {
    public class CurrentGauntletManager : ICurrentGauntletManager {

        private bool mComingFromGauntletVictory = false;
        public bool ComingFromGauntletVictory { get { return mComingFromGauntletVictory; } set { mComingFromGauntletVictory = value; } }
    }
}