using Zenject;

namespace MonsterMatch {
    public class Treasure : ITreasure {
        readonly ITreasureDataManager mManager;

        private ITreasureData mData;

        public Treasure( ITreasureDataManager i_manager, ITreasureData i_data ) {
            mManager = i_manager;
            mData = i_data;
        }

        public int GetValue() {
            return mManager.GetValueForRarity( mData.GetRarity() );
        }

        public class Factory : Factory<ITreasureData, Treasure> { }
    }
}
