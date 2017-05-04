
namespace MonsterMatch {
    public interface ITimedChestSaveDataEntry {
        string GetId();
        long GetNextAvailableTime();
        void SetNextAvailableTime( long i_timeMs );
    }
}