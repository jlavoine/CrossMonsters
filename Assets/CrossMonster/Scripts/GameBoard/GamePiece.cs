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
            RotatePiece();            
        }

        public void Randomize() {
            int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.GetPieceTypes() );
            SetPieceType( randomPieceType );
        }

        private void RotatePiece() {
            Randomize();
        }

        private void SetPieceType( int i_pieceType ) {
            PieceType = i_pieceType;
            SendModelChangedEvent();
        }

        public class Factory : Factory<int, GamePiece> {}
    }
}
