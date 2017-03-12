using System.Collections.Generic;

namespace CrossMonsters {
    public class DungeonGameSessionData : IDungeonGameSessionData {
        public List<string> Monsters;        

        public List<string> GetMonsters() {
            return Monsters;
        }
    }
}
