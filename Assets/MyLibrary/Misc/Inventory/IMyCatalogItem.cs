using System.Collections.Generic;

namespace MyLibrary {
    public interface IMyCatalogItem {
        string GetId();
        List<string> GetTags();
        bool HasTag( string i_tag );
    }
}