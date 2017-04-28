using MyLibrary;
using Zenject;
using System;

namespace MonsterMatch {
    public class TimedChestPM : PresentationModel, ITimedChestPM {
        public const string NAME_PROPERTY = "Name";
        public const string CURRENT_KEYS_PROPERTY = "CurrentKeys";
        public const string REQUIRED_KEYS_PROPERTY = "RequiredKeys";
        public const string AVAILABLE_PROPERTY = "IsAvailable";
        public const string UNAVAILABLE_PROPERTY = "IsUnavailable";
        public const string COUNTDOWN_PROPERTY = "Countdown";
        public const string COUNTDOWN_FORMAT_PROPERTY = "CountdownFormat";
        public const string CAN_OPEN_PROPERTY = "CanOpen";

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
            SetCanOpenProperty();
            SetCountdownFormatProperty();
            UpdateAvailability();
            CreateCountdownIfUnavailable();
        }

        public string GetCountdownTimeFormatted( long i_remainingTimeInMs ) {
            TimeSpan ts = TimeSpan.FromMilliseconds( i_remainingTimeInMs );
            if ( ts.TotalDays > 1 ) {
                return (int)ts.TotalDays + " days, " + ts.Hours + " hours";
            } else if ( ts.TotalHours > 1 ) {
                return (int)ts.TotalHours + " hours, " + ts.Minutes + " minutes";
            } else {
                return (int)ts.TotalMinutes + " minutes, " + ts.Seconds + " seconds";
            }
        }

        private void SetName() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        private void SetKeyProgress() {
            ViewModel.SetProperty( CURRENT_KEYS_PROPERTY, mSaveData.GetCurrentKeysForChest( mData.GetKeyId() ).ToString() );
            ViewModel.SetProperty( REQUIRED_KEYS_PROPERTY, mData.GetKeysRequired().ToString() );
        }

        private void SetCanOpenProperty() {
            bool canOpen = mSaveData.CanOpenChest( mData );
            ViewModel.SetProperty( CAN_OPEN_PROPERTY, canOpen );
        }

        private void UpdateAvailability() {
            bool isAvailable = IsAvailable();
            ViewModel.SetProperty( AVAILABLE_PROPERTY, isAvailable );
            ViewModel.SetProperty( UNAVAILABLE_PROPERTY, !isAvailable );
        }

        private void CreateCountdownIfUnavailable() {
            if ( !IsAvailable() ) {
                mCountdownUntilAvailable = mCountdownSpawner.Create( mSaveData.GetNextAvailableTime( mData.GetId() ), new CountdownCallback( UpdateAvailability ) );
                ViewModel.SetProperty( COUNTDOWN_PROPERTY, mCountdownUntilAvailable );
            }
        }

        private bool IsAvailable() {
            return mSaveData.IsChestAvailable( mData.GetId() );
        }

        private void SetCountdownFormatProperty() {
            Action<long, Action<string>> action = FormatCountdown;
            ViewModel.SetProperty( COUNTDOWN_FORMAT_PROPERTY, action );
        }

        private void FormatCountdown( long i_remainingTimeMs, Action<string> i_callback ) {
            string text = GetCountdownTimeFormatted( i_remainingTimeMs );
            i_callback( text );
        }

        public class Factory : Factory<ITimedChestData, TimedChestPM> { }
    }
}
