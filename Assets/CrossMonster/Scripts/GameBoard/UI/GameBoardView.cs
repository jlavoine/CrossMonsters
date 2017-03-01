using MyLibrary;
using UnityEngine;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameBoardView : GroupView {

        public GameObject GamePieceViewPrefab;

        private GameBoardPM mPM;

        void Start() {
            mPM = new GameBoardPM( new GameBoard() );

            CreateGamePieceViews();

            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        private void CreateGamePieceViews() {
            List<IGamePiecePM> gamePiecePMs = mPM.GamePiecePMs;
            foreach ( IGamePiecePM piecePM in gamePiecePMs ) {
                GameObject pieceObject = gameObject.InstantiateUI( GamePieceViewPrefab, gameObject );
                GamePieceView pieceView = pieceObject.GetComponent<GamePieceView>();
                pieceView.Init( piecePM );
            }
        }
    }
}