using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class TimedChestPM : PresentationModel, ITimedChestPM {
        public const string NAME_PROPERTY = "Name";
        public const string CURRENT_KEYS_PROPERTY = "CurrentKeys";
        public const string REQUIRED_KEYS_PROPERTY = "RequiredKeys";
        public const string AVAILABLE_PROPERTY = "IsAvailable";
        public const string UNAVAILABLE_PROPERTY = "IsUnavailable";

        readonly IStringTableManager mStringTable;
        readonly ITimedChestSaveData mSaveData;

        private ITimedChestData mData;

        public TimedChestPM( IStringTableManager i_stringTable, ITimedChestSaveData i_saveData, ITimedChestData i_data ) {
            mStringTable = i_stringTable;
            mSaveData = i_saveData;
            mData = i_data;

            SetName();
            SetKeyProgress();
            UpdateAvailability();
        }

        private void SetName() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        private void SetKeyProgress() {
            ViewModel.SetProperty( CURRENT_KEYS_PROPERTY, mSaveData.GetCurrentKeysForChest( mData.GetKeyId() ).ToString() );
            ViewModel.SetProperty( REQUIRED_KEYS_PROPERTY, mData.GetKeysRequired().ToString() );
        }

        private void UpdateAvailability() {
            bool isAvailable = mSaveData.IsChestAvailable( mData.GetId() );
            ViewModel.SetProperty( AVAILABLE_PROPERTY, isAvailable );
            ViewModel.SetProperty( UNAVAILABLE_PROPERTY, !isAvailable );
        }

        public class Factory : Factory<ITimedChestData, TimedChestPM> { }
    }
}
