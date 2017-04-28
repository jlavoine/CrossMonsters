using System.Collections.Generic;

namespace MyLibrary {
    public class PlayerInventoryManager : IPlayerInventoryManager {

        private IBasicBackend mBackend;

        private Dictionary<string, IMyCatalogItem> mCatalog;

        private List<IMyItemInstance> mInventory;

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadItemCatalogAndPlayerInventory();           
        }

        private void DownloadItemCatalogAndPlayerInventory() {
            UnityEngine.Debug.LogError( "Downloading catalog and inv" );

            mBackend.GetItemCatalog( ( catalogResult ) => {
                mCatalog = catalogResult;                
                mBackend.GetInventory( ( inventoryResult ) => {
                    mInventory = inventoryResult;
                } );
            } );
        }
    }
}