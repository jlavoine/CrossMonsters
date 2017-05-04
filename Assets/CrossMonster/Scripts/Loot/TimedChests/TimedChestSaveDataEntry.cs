
namespace MonsterMatch {
    public class TimedChestSaveDataEntry : ITimedChestSaveDataEntry {
        public string Id;
        public long NextAvailableTime;

        public string GetId() {
            return Id;
        }

        public long GetNextAvailableTime() {
            return NextAvailableTime;
        }

        public void SetNextAvailableTime( long i_timeMs ) {
            NextAvailableTime = i_timeMs;
        }
    }
}