using MyLibrary;

namespace MonsterMatch {
    public class AttackComboPiecePM : PresentationModel, IAttackComboPiecePM {
        public const string PIECE_TYPE_PROPERTY = "PieceType";

        public AttackComboPiecePM( int i_pieceType ) {
            SetPieceTypeProperty( i_pieceType );
        }

        private void SetPieceTypeProperty( int i_pieceType ) {
            ViewModel.SetProperty( PIECE_TYPE_PROPERTY, i_pieceType.ToString() );
        }
    }
}