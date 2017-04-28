using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class TimedChestPM : PresentationModel, ITimedChestPM {
        public const string NAME_PROPERTY = "Name";
        public const string CURRENT_KEYS_PROPERTY = "CurrentKeys";
        public const string REQUIRED_KEYS_PROPERTY = "RequiredKeys";

        readonly IStringTableManager mStringTable;
        readonly IPlayerInventoryManager mInventory;

        private ITimedChestData mData;

        public TimedChestPM( IStringTableManager i_stringTable, IPlayerInventoryManager i_inventory, ITimedChestData i_data ) {
            mStringTable = i_stringTable;
            mInventory = i_inventory;
            mData = i_data;

            SetName();
            SetKeyProgress();
        }

        private void SetName() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        private void SetKeyProgress() {
            ViewModel.SetProperty( CURRENT_KEYS_PROPERTY, mInventory.GetItemCount( mData.GetKeyId() ).ToString() );
            ViewModel.SetProperty( REQUIRED_KEYS_PROPERTY, mData.GetKeysRequired().ToString() );
        }

        public class Factory : Factory<ITimedChestData, TimedChestPM> { }
    }
}
