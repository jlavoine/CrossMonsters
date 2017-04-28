
namespace MonsterMatch {
    public class TimedChestData : ITimedChestData {
        public string Id;
        public string KeyId;
        public string ResetType;

        public float KeyDropRate;
        public int KeysRequired;   
        
        public string GetId() {
            return Id;
        }    

        public string GetNameKey() {
            return Id + "TimedChest_Name";
        }

        public string GetKeyId() {
            return KeyId;
        }

        public int GetKeysRequired() {
            return KeysRequired;
        }
    }
}
