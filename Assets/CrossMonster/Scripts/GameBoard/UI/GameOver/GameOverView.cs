using MyLibrary;

namespace CrossMonsters {
    public class GameOverView : GroupView {
        private GameOverPM mPM;

        void Start() {
            mPM = new GameOverPM();
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}