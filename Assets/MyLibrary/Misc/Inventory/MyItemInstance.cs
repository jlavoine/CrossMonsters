using UnityEngine;

namespace MyLibrary {
    public class MyItemInstance : IMyItemInstance {
        public string Id;
        public int Count;
        public IMyCatalogItem CatalogItem;

        public void SetCatalogItem( IMyCatalogItem i_catalogItem ) {
            CatalogItem = i_catalogItem;
        }

        public string GetId() {
            return Id;
        }

        public int GetCount() {
            return Count;
        }

        public void RemoveUses( int i_count ) {
            Count = Mathf.Max( 0, Count - i_count );
        }
    }
}