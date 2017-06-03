﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyLibrary {
    public class MyCatalogItem : IMyCatalogItem {
        public string Id;
        public List<string> Tags;
        public string CustomData;

        public string GetId() {
            return Id;
        }

        public T GetCustomData<T>() {
            return JsonConvert.DeserializeObject<T>( CustomData );
        }

        public List<string> GetTags() {
            return Tags;
        }

        public bool HasTag( string i_tag ) {
            return Tags.Contains( i_tag );
        }
    }
}