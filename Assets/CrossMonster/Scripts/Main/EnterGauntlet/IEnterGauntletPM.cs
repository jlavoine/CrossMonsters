using MyLibrary;

namespace MonsterMatch {
    public interface IEnterGauntletPM : IBasicWindowPM {
        void SetIndex( int i_index );
        void EnterGauntlet( int i_difficulty );
    }
}