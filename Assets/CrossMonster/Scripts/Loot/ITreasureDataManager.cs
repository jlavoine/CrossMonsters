using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public interface ITreasureDataManager {
        void Init( IBasicBackend i_backend );

        List<ITreasureSetData> TreasureSetData { get; }

        bool DoesPlayerHaveTreasure( string i_treasureId );
    }
}
