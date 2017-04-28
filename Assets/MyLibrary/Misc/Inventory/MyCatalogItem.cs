using System.Collections.Generic;

namespace MyLibrary {
    public class MyCatalogItem : IMyCatalogItem {
        public string Id;
        public List<string> Tags;

        public string GetId() {
            return Id;
        }

        public List<string> GetTags() {
            return Tags;
        }

        public bool HasTag( string i_tag ) {
            return Tags.Contains( i_tag );
        }
    }
}