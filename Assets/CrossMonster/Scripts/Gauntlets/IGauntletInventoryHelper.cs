using MyLibrary;

namespace MonsterMatch {
    public interface IGauntletInventoryHelper {
        IMyItemInstance GetGauntletKeysFromIndex( int i_index );

        void ConsumeGauntletKeyForIndex( int i_index );
    }
}
