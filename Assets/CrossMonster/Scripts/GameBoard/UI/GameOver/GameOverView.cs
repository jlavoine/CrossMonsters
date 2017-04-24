using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class GameOverView : GroupView {
        private GameOverPM mPM;

        [Inject]
        IStringTableManager StringTableManager;

        void Start() {
            mPM = new GameOverPM( StringTableManager );
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}