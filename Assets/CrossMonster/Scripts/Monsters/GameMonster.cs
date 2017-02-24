
namespace CrossMonsters {
    public class GameMonster : IGameMonster {
        private int mRemainingHP;
        public int RemainingHP { get { return mRemainingHP; } set { mRemainingHP = value; } }

        public GameMonster( IMonsterData i_data ) {
            SetStartingHP( i_data );
        }

        private void SetStartingHP( IMonsterData i_data ) {
            RemainingHP = i_data.GetMaxHP();
        }
    }
}
