using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class GameBoardPM : PresentationModel, IInitializable {
        [Inject]
        IGameBoard GameBoard;

        private List<IGamePiecePM> mGamePiecePMs = new List<IGamePiecePM>();
        public List<IGamePiecePM> GamePiecePMs { get { return mGamePiecePMs; } set { GamePiecePMs = value; } }

        public void Initialize() {
            CreateGamePiecePMs();
        }

        public override void Dispose() {

        }

        private void CreateGamePiecePMs() {
            foreach ( IGamePiece piece in GameBoard.BoardPieces ) {
                IGamePiecePM piecePM = new GamePiecePM( piece );
                GamePiecePMs.Add( piecePM );
            }
        }
    }
}
