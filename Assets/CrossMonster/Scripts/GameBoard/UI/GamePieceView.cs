using MyLibrary;

namespace CrossMonsters {
    public class GamePieceView : GroupView {

        private GamePiecePM mPM;

        void Init( GamePiecePM i_pm ) {
            mPM = i_pm;

            SetModel( i_pm.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}
