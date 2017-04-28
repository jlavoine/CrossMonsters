using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class TimedChestPM : PresentationModel, ITimedChestPM {
        public const string NAME_PROPERTY = "Name";
        public const string CURRENT_KEYS_PROPERTY = "CurrentKeys";
        public const string REQUIRED_KEYS_PROPERTY = "RequiredKeys";
        public const string AVAILABLE_PROPERTY = "IsAvailable";
        public const string UNAVAILABLE_PROPERTY = "IsUnavailable";
        public const string COUNTDOWN_PROPERTY = "Countdown";

        readonly IStringTableManager mStringTable;
        readonly ITimedChestSaveData mSaveData;
        readonly IMyCountdown_Spawner mCountdownSpawner;

        private ITimedChestData mData;
        private IMyCountdown mCountdownUntilAvailable;

        public TimedChestPM( IStringTableManager i_stringTable, ITimedChestSaveData i_saveData, IMyCountdown_Spawner i_countdownSpawner, ITimedChestData i_data ) {
            mStringTable = i_stringTable;
            mCountdownSpawner = i_countdownSpawner;
            mSaveData = i_saveData;
            mData = i_data;

            SetName();
            SetKeyProgress();
            UpdateAvailability();
            CreateCountdownIfUnavailable();
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
            bool isAvailable = IsAvailable();
            ViewModel.SetProperty( AVAILABLE_PROPERTY, isAvailable );
            ViewModel.SetProperty( UNAVAILABLE_PROPERTY, !isAvailable );
        }

        private void CreateCountdownIfUnavailable() {
            if ( !IsAvailable() ) {
                mCountdownUntilAvailable = mCountdownSpawner.Create( mSaveData.GetNextAvailableTime( mData.GetId() ), new CountdownCallback( OnAvailable ) );
                ViewModel.SetProperty( COUNTDOWN_PROPERTY, mCountdownUntilAvailable );
            }
        }

        private void OnAvailable() {

        }

        private bool IsAvailable() {
            return mSaveData.IsChestAvailable( mData.GetId() );
        }

        public class Factory : Factory<ITimedChestData, TimedChestPM> { }
    }
}
