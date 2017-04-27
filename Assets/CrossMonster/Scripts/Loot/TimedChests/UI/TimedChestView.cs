using MyLibrary;

namespace MonsterMatch {
    public class TimedChestView : GroupView {

        public void Init( ITimedChestPM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}