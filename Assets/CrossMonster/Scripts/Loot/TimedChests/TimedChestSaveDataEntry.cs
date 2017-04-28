
namespace MonsterMatch {
    public class TimedChestSaveDataEntry : ITimedChestSaveDataEntry {
        public string Id;
        public double NextAvailableTime;

        public string GetId() {
            return Id;
        }

        public double GetNextAvailableTime() {
            return NextAvailableTime;
        }
    }
}