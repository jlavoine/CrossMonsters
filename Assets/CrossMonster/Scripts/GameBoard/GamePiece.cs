using Zenject;
using MyLibrary;

namespace CrossMonsters {
    public class GamePiece : BusinessModel, IGamePiece {
        [Inject]
        IGameRules GameRules;

        private int mPieceType;
        public int PieceType { get { return mPieceType; } set { mPieceType = value; } }

        public GamePiece( int i_pieceType ) {
            PieceType = i_pieceType;
        }

        public void UsePiece() {
            RotatePieceType();
            SendModelChangedEvent();
        }

        private void RotatePieceType() {
            PieceType = GameRules.GetGamePieceRotation( PieceType );
        }

        public class Factory : Factory<int, GamePiece> {}
    }
}
