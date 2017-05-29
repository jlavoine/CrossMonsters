using MyLibrary;

namespace MonsterMatch {
    public interface IEnterDungeonPM : IBasicWindowPM {
        void SetRequestedDungeon( string i_gameType, int i_areaId, int i_dungeonId );
        void LoadDungeon();
    }
}
