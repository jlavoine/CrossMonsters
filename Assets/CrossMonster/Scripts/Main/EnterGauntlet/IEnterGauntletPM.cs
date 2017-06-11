using MyLibrary;

namespace MonsterMatch {
    public interface IEnterGauntletPM : IBasicWindowPM {
        void SetIndex( int i_index );
        void EnterGauntlet( GauntletDifficulties i_difficulty );
    }
}