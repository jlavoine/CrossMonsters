using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameBoardPM : PresentationModel {

        private IGameBoard mBoard;

        private List<IGamePiecePM> mGamePiecePMs = new List<IGamePiecePM>();
        public List<IGamePiecePM> GamePiecePMs { get { return mGamePiecePMs; } set { GamePiecePMs = value; } }

        public GameBoardPM( IGameBoard i_board ) {
            mBoard = i_board;

            CreateGamePiecePMs();
        }

        public override void Dispose() {

        }

        private void CreateGamePiecePMs() {
            foreach ( IGamePiece piece in mBoard.BoardPieces ) {
                IGamePiecePM piecePM = new GamePiecePM( piece );
                GamePiecePMs.Add( piecePM );
            }
        }
    }
}
