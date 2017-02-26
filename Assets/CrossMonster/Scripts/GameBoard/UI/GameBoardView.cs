using MyLibrary;
using UnityEngine;

namespace CrossMonsters {
    public class GameBoardView : GroupView {

        public GameObject GamePiecePrefab;

        private GameBoardPM mPM;

        void Start() {
            mPM = new GameBoardPM();

            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}