using MyLibrary;

namespace MonsterMatch {
    public class TreasureView : GroupView {

        public void Init( ITreasurePM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}