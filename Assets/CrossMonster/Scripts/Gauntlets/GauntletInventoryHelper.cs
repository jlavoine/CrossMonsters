using MyLibrary;

namespace MonsterMatch {
    public class GauntletInventoryHelper : IGauntletInventoryHelper {
        public const string ITEM_KEY = "Gauntlet_Key_";

        readonly IPlayerInventoryManager mInventory;

        public GauntletInventoryHelper( IPlayerInventoryManager i_inventory ) {
            mInventory = i_inventory;
        }

        public IMyItemInstance GetGauntletKeysFromIndex( int i_index ) {
            return mInventory.GetItem( ITEM_KEY + i_index );
        }
    }
}
