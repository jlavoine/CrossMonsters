using MyLibrary;

namespace MonsterMatch {
    public class EnterGauntletPM : BasicWindowPM, IEnterGauntletPM {
        public const string KEY_COUNT_PROPERTY = "KeyCount";

        readonly IGauntletInventoryHelper mInventory;

        private int mIndex;
        public int Index { get { return mIndex; } set { mIndex = value; } }

        public EnterGauntletPM( IGauntletInventoryHelper i_inventory ) {
            mInventory = i_inventory;

            Hide();
        }

        public void SetIndex( int i_index ) {
            Index = i_index;
            UpdateProperties();
        }

        private void UpdateProperties() {
            UpdateKeyCount();
        }

        private void UpdateKeyCount() {
            IMyItemInstance keys = mInventory.GetGauntletKeysFromIndex( Index );
            if ( keys != null ) {
                ViewModel.SetProperty( KEY_COUNT_PROPERTY, keys.GetCount() );
            } else {
                ViewModel.SetProperty( KEY_COUNT_PROPERTY, 0 );
            }
        }
    }
}
