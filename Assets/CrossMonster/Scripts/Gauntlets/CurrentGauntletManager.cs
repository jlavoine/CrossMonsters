
namespace MonsterMatch {
    public class CurrentGauntletManager : ICurrentGauntletManager {

        readonly ICurrentDungeonGameManager mDungeonManager;

        private int mCurrentGauntletIndex = 0;
        public int CurrentGauntletIndex { get { return mCurrentGauntletIndex; } set { mCurrentGauntletIndex = value; } }

        private bool mComingFromGauntletVictory = false;
        public bool ComingFromGauntletVictory { get { return mComingFromGauntletVictory; } set { mComingFromGauntletVictory = value; } }

        private IGauntletProgress mProgress;
        public IGauntletProgress Progress { get { return mProgress; } set { mProgress = value; } }

        public CurrentGauntletManager( ICurrentDungeonGameManager i_dungeonManager ) {
            mDungeonManager = i_dungeonManager;
        }

        public bool IsGauntletSessionInProgress() {
            if ( mDungeonManager.Data != null ) {
                return mDungeonManager.Data.GetGameMode() == EnterGauntletPM.GAUNTLET_GAME_TYPE;
            } else {
                return false;
            }
        }
    }
}