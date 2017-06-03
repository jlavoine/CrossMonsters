using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class ExpeditionUnit : IExpeditionUnit {
        private IMyItemInstance mItem;
        private IExpeditionUnitCustomData mData;

        public ExpeditionUnit( IMyItemInstance i_itemInstance, IExpeditionUnitCustomData i_data ) {
            mItem = i_itemInstance;
            mData = i_data;
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

        public class Factory : Factory<IMyItemInstance, IExpeditionUnitCustomData, ExpeditionUnit> { }
    }
}
