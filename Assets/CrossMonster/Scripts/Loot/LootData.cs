using System.Collections.Generic;

namespace CrossMonsters {
    public class LootData : ILootData {
        public string Id;
        public LootTypes LootType;   
        
        public string GetId() {
            return Id;
        }             
    }
}