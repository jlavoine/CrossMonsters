using MyLibrary;

namespace MonsterMatch {
    public class EnterGauntletPM : BasicWindowPM, IEnterGauntletPM {
        public const string KEY_COUNT_PROPERTY = "KeyCount";
        public const string CAN_ENTER_GAUNTLET_PROPERTY = "CanEnter";

        public const string GAUNTLET_GAME_TYPE = "Gauntlet";

        readonly IGauntletInventoryHelper mInventory;
        readonly IDungeonLoader mDungeonLoader;
        readonly ICurrentGauntletManager mGauntletManager;

        private int mIndex;
        public int Index { get { return mIndex; } set { mIndex = value; } }

        public EnterGauntletPM( ICurrentGauntletManager i_gauntletManager, IGauntletInventoryHelper i_inventory, IDungeonLoader i_dungeonLoader ) {
            mInventory = i_inventory;
            mDungeonLoader = i_dungeonLoader;
            mGauntletManager = i_gauntletManager;

            Hide();
            SetCanEnterProperty( false );
        }

        public void SetIndex( int i_index ) {
            Index = i_index;
            UpdateProperties();
        }

        public void EnterGauntlet( int i_difficulty ) {
            mGauntletManager.CurrentGauntletIndex = Index;
            mDungeonLoader.LoadDungeon( GAUNTLET_GAME_TYPE, Index, i_difficulty );
            mInventory.ConsumeGauntletKeyForIndex( Index );
            Hide();
        }

        private void UpdateProperties() {
            UpdateKeyCount();
            UpdateCanEnter();
        }

        private void UpdateKeyCount() {
            IMyItemInstance keys = mInventory.GetGauntletKeysFromIndex( Index );
            if ( keys != null ) {
                ViewModel.SetProperty( KEY_COUNT_PROPERTY, keys.GetCount() );
            } else {
                ViewModel.SetProperty( KEY_COUNT_PROPERTY, 0 );
            }
        }

        private void UpdateCanEnter() {
            IMyItemInstance keys = mInventory.GetGauntletKeysFromIndex( Index );
            bool canEnter = keys.GetCount() > 0;
            SetCanEnterProperty( canEnter );
        }

        private void SetCanEnterProperty( bool i_can ) {
            ViewModel.SetProperty( CAN_ENTER_GAUNTLET_PROPERTY, i_can );
        }
    }
}
