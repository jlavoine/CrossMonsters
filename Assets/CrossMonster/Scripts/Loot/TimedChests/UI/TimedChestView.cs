using MyLibrary;

namespace MonsterMatch {
    public class TimedChestView : GroupView {
        private ITimedChestPM mPM;

        public void Init( ITimedChestPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );
        }

        public void OpenChestClicked() {
            mPM.Open();
        }
    }
}