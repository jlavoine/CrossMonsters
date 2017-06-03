using System.Collections.Generic;

namespace MyLibrary {
    public interface IMyCatalogItem {
        string GetId();
        T GetCustomData<T>();

        List<string> GetTags();

        bool HasTag( string i_tag );
    }
}