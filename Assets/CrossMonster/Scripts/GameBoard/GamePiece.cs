using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class GamePiece : BusinessModel, IGamePiece {
        [Inject]
        IGameRules GameRules;

        [Inject]
        ICurrentDungeonGameManager DungeonManager;

        private int mPieceType;
        public int PieceType { get { return mPieceType; } set { mPieceType = value; } }

        private int mIndex;
        public int Index { get { return mIndex; } set { mIndex = value; } }

        private GamePieceStates mState;
        public GamePieceStates State { get { return mState; } set { mState = value; } }

        public GamePiece( int i_pieceType, int i_pieceIndex ) {
            Index = i_pieceIndex;
            PieceType = i_pieceType;
            State = GamePieceStates.Selectable;
        }

        public void UsePiece() {
            if ( DungeonManager.Data.ShouldRotatePieces() ) {
                RotatePiece();                
            }

            SetPieceState( GamePieceStates.Correct );
            SendModelChangedEvent();
        }

        public void PieceFailedMatch() {
            SetPieceState( GamePieceStates.Incorrect );
            SendModelChangedEvent();
        }

        public void Randomize() {
            int randomPieceType = ListUtils.GetRandomElement<int>( GameRules.GetPieceTypes() );
            SetPieceType( randomPieceType );
            SendModelChangedEvent();
        }

        private void RotatePiece() {
            Randomize();
        }

        private void SetPieceType( int i_pieceType ) {
            PieceType = i_pieceType;            
        }
        
        private void SetPieceState( GamePieceStates i_state ) {
            State = i_state;
        }

        public class Factory : Factory<int, int, GamePiece> {}
    }
}
