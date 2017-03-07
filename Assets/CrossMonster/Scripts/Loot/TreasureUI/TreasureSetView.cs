using MyLibrary;

namespace CrossMonsters {
    public class TreasureSetView : GroupView {

        public void Init( ITreasureSetPM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}
