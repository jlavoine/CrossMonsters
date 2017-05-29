using MyLibrary;

namespace MonsterMatch {
    public class EnterDungeonPM : BasicWindowPM, IEnterDungeonPM {
        private string mGameType;
        private int mAreaId;
        private int mDungeonId;

        public string GameType { get { return mGameType; } set { mGameType = value; } }
        public int AreaId { get { return mAreaId; } set { mAreaId = value; } }        
        public int DungeonId { get { return mDungeonId; } set { mDungeonId = value;} }

        readonly IDungeonLoader mDungeonLoader;

        public EnterDungeonPM( IDungeonLoader i_dungeonLoader ) {
            mDungeonLoader = i_dungeonLoader;

            Hide();
        }

        public void SetRequestedDungeon( string i_gameType, int i_areaId, int i_dungeonId ) {
            GameType = i_gameType;
            AreaId = i_areaId;
            DungeonId = i_dungeonId;
        }

        public void LoadDungeon() {
            mDungeonLoader.LoadDungeon( GameType, AreaId, DungeonId );
            Hide();
        }
    }
}
