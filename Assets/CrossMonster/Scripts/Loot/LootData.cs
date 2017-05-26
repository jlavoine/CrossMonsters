using System.Collections.Generic;

namespace MonsterMatch {
    public class LootData : ILootData {
        public string Id;
        public LootTypes LootType;
        public string Rarity;
        public List<string> Categories;

        public string GetId() {
            return Id;
        }             

        public string GetRarity() {
            return Rarity;
        }
    }
}