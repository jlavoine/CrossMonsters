using MyLibrary;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class GameBoardView : GroupView {
        [Inject]
        DiContainer container;

        public GameObject GamePieceViewPrefab;

        [Inject]
        GameBoardPM PM;

        void Start() {
            CreateGamePieceViews();

            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }

        private void CreateGamePieceViews() {
            List<IGamePiecePM> gamePiecePMs = PM.GamePiecePMs;
            foreach ( IGamePiecePM piecePM in gamePiecePMs ) {
                GameObject pieceObject = container.InstantiatePrefab( GamePieceViewPrefab, transform );
                //GameObject pieceObject = gameObject.InstantiateUI( GamePieceViewPrefab, gameObject );
                GamePieceView pieceView = pieceObject.GetComponent<GamePieceView>();
                pieceView.Init( piecePM );
            }
        }
    }
}