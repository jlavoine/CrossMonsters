using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface ITimedChestDataManager {
        void Init( IBasicBackend i_backend );

        List<ITimedChestData> TimedChestData { get; }
    }
}
