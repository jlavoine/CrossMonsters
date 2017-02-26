using MyLibrary;

namespace CrossMonsters {
    public class MonsterView : GroupView {
        private IMonsterPM mPM;

        public void Init( IMonsterPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );
        }
    }
}
