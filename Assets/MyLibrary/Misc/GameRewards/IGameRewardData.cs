using System.Collections.Generic;

namespace MyLibrary {
    public interface IGameRewardData {
        string GetId();
        string GetLootType();        

        int GetCount();
    }
}