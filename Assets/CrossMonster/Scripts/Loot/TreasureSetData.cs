using System.Collections.Generic;

namespace CrossMonsters {
    public class TreasureSetData : ITreasureSetData {
        public string Id;
        public List<string> TreasureInSet;

        public string GetId() {
            return Id;
        }

        public List<string> GetTreasuresInSet() {
            return TreasureInSet;
        }
    }
}
