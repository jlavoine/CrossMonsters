using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class BoostUnit : IBoostUnit {
        private IMyItemInstance mItem;
        private IBoostUnitCustomData mData;

        public BoostUnit( IMyItemInstance i_itemInstance, IBoostUnitCustomData i_data ) {
            mItem = i_itemInstance;
            mData = i_data;

            if ( mData == null ) {
                mData = new BoostUnitCustomData(); // this is an ultimate safeguard, not sure how else to better do this
            }
        }

        public bool HasEffect( string i_effectId ) {
            return mData.HasEffect( i_effectId );
        }

        public int GetEffect( string i_effectId ) {
            if ( HasEffect( i_effectId ) ) {
                int totalValue = mItem.GetCount() * mData.GetEffect( i_effectId );
                return totalValue;
            } else {
                return 0;
            }
        }

        public class Factory : Factory<IMyItemInstance, IBoostUnitCustomData, BoostUnit> { }
    }
}
