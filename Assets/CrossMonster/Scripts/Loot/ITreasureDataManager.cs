using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface ITreasureDataManager {
        void Init( IBasicBackend i_backend );

        List<ITreasureSetData> TreasureSetData { get; }

        bool DoesPlayerHaveTreasure( string i_treasureId );

        int GetValueForRarity( string i_rarity );
        int GetPlayerTreasureLevel();

        float GetPlayerTreasureLevelProgress();
    }
}
