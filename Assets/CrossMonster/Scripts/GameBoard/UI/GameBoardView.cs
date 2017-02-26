using MyLibrary;
using UnityEngine;

namespace CrossMonsters {
    public class GameBoardView : GroupView {

        public GameObject GamePieceViewPrefab;

        private GameBoardPM mPM;

        void Start() {
            mPM = new GameBoardPM( new GameBoard() );

            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}