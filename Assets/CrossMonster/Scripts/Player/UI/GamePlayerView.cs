using MyLibrary;

namespace CrossMonsters {
    public class GamePlayerView : GroupView {
        private IGamePlayerPM mPM;

        void Start() {
            IPlayerData playerData = GetPlayerData();
            IGamePlayer player = new GamePlayer( playerData );

            mPM = new GamePlayerPM( player );
            SetModel( mPM.ViewModel );
        }

        // TODO this is just temp testing
        public IPlayerData GetPlayerData() {
            PlayerData data = new PlayerData();
            data.HP = 100;

            return data;
        }
    }
}
