using MyLibrary;

namespace MonsterMatch {
    public class GauntletInventoryHelper : IGauntletInventoryHelper {
        public const string ITEM_KEY = "Gauntlet_Key_";

        readonly IPlayerInventoryManager mInventory;

        public GauntletInventoryHelper( IPlayerInventoryManager i_inventory ) {
            mInventory = i_inventory;
        }

        public IMyItemInstance GetGauntletKeysFromIndex( int i_index ) {
            string itemKey = GetGauntletItemKeyForIndex( i_index );
            return mInventory.GetItem( itemKey );
        }

        public void ConsumeGauntletKeyForIndex( int i_index ) {
            string itemKey = GetGauntletItemKeyForIndex( i_index );
            mInventory.RemoveUsesFromItem( itemKey, 1 );
        }

        public string GetGauntletItemKeyForIndex( int i_index ) {
            return ITEM_KEY + i_index;
        }
    }
}
