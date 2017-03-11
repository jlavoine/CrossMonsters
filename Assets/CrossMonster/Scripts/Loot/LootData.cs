using System.Collections.Generic;

namespace CrossMonsters {
    public class LootData : ILootData {
        public string Id;
        public LootTypes LootType;
        public string Rarity;
        public List<string> Categories;

        public string GetId() {
            return Id;
        }             
    }
}