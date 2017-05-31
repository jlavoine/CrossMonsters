using System.Collections.Generic;

namespace MyLibrary {
    public class PlayerInventoryManager : IPlayerInventoryManager {

        private IBasicBackend mBackend;

        private IMyItemCatalog mCatalog;

        private Dictionary<string, IMyItemInstance> mInventory;
        public Dictionary<string, IMyItemInstance> Inventory { get { return mInventory; } set { mInventory = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadItemCatalogAndPlayerInventory();           
        }

        public int GetItemCount( string i_itemId ) {
            if ( Inventory.ContainsKey( i_itemId ) ) {
                return Inventory[i_itemId].GetCount();
            } else {
                return 0;
            }
        }

        public void RemoveUsesFromItem( string i_itemId, int i_count ) {
            if ( Inventory.ContainsKey( i_itemId ) ) {
                Inventory[i_itemId].RemoveUses( i_count );
            }
        }

        private void DownloadItemCatalogAndPlayerInventory() {
            mBackend.GetItemCatalog( ( catalogResult ) => {
                mCatalog = catalogResult;
                mBackend.GetInventory( ( inventoryResult ) => {
                    Inventory = new Dictionary<string, IMyItemInstance>();
                    foreach ( IMyItemInstance item in inventoryResult ) {
                        item.SetCatalogItem( mCatalog.GetItem( item.GetId() ) );
                        Inventory.Add( item.GetId(), item );                        
                    }                    
                } );
            } );
        }
    }
}