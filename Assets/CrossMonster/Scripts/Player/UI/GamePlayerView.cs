using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GamePlayerView : GroupView {
        private IGamePlayerPM mPM;

        void Start() {
            IPlayerData playerData = GetPlayerData();
            IGamePlayer player = new GamePlayer( playerData );

            mPM = new GamePlayerPM( player );
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        // TODO this is just temp testing
        public IPlayerData GetPlayerData() {
            PlayerData data = new PlayerData();
            data.HP = 100;
            data.Defenses = new Dictionary<int, int>() { { 0, 5 }, { 1, 5 } };

            return data;
        }
    }
}
