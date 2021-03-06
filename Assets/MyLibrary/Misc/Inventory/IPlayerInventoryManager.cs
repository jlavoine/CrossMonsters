﻿using System.Collections.Generic;

namespace MyLibrary {
    public interface IPlayerInventoryManager {
        void Init( IBasicBackend i_backend );

        void RemoveUsesFromItem( string i_itemId, int i_count );

        int GetItemCount( string i_itemId );

        IMyItemInstance GetItem( string i_id );
        List<IMyItemInstance> GetItemsWithTag( string i_tag );
    }
}