using System.Collections.Generic;

namespace CrossMonsters {
    public interface ITreasureSetData {
        string GetId();

        List<string> GetTreasuresInSet();
    }
}
