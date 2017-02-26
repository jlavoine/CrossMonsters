using MyLibrary;

namespace CrossMonsters {
    public class AttackComboPieceView : GroupView {
        private IAttackComboPiecePM mPM;

        public void Init( int i_pieceType ) {
            mPM = new AttackComboPiecePM( i_pieceType );

            SetModel( mPM.ViewModel );
        }
    }
}
