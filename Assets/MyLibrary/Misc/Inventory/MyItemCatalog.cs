using System.Collections.Generic;

namespace MyLibrary {
    public class MyItemCatalog : IMyItemCatalog {
        private Dictionary<string, IMyCatalogItem> mItems = new Dictionary<string, IMyCatalogItem>();
        public Dictionary<string, IMyCatalogItem> Items { get { return mItems; } set { mItems = value; } }

        public MyItemCatalog( Dictionary<string, IMyCatalogItem> i_items ) {
            Items = i_items;
        }

        public IMyCatalogItem GetItem( string i_id ) {
            if ( Items.ContainsKey( i_id ) ) {
                return Items[i_id];
            } else {
                return null;
            }
        }
    }
}
