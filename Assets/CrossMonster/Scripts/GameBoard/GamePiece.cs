
namespace CrossMonsters {
    public class GamePiece : IGamePiece {
        private int mPieceType;
        public int PieceType { get { return mPieceType; } set { mPieceType = value; } }

        public GamePiece( int i_pieceType ) {
            PieceType = i_pieceType;
        }

        public void UsePiece() {
            RotatePieceType();
        }

        private void RotatePieceType() {
            PieceType = GameRules.Instance.GetGamePieceRotation( PieceType );
        }
    }
}
