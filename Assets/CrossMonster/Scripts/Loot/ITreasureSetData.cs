using System.Collections.Generic;

namespace MonsterMatch {
    public interface ITreasureSetData {
        string GetId();

        List<string> GetTreasuresInSet();
    }
}
