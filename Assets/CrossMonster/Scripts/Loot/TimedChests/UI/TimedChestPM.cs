using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class TimedChestPM : PresentationModel, ITimedChestPM {
        public const string NAME_PROPERTY = "Name";

        readonly IStringTableManager mStringTable;

        private ITimedChestData mData;

        public TimedChestPM( IStringTableManager i_stringTable, ITimedChestData i_data ) {
            mStringTable = i_stringTable;
            mData = i_data;

            SetName();
        }

        private void SetName() {
            string text = mStringTable.Get( mData.GetNameKey() );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        public class Factory : Factory<ITimedChestData, TimedChestPM> { }
    }
}
