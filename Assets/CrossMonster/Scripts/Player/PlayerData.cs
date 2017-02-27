using System.Collections.Generic;

namespace CrossMonsters {
    public class PlayerData : IPlayerData {
        public int HP;
        public Dictionary<int, int> Defenses;

        public int GetHP() {
            return HP;
        }

        public int GetDefenseForType( int i_type ) {
            if ( Defenses.ContainsKey( i_type ) ) {
                return Defenses[i_type];
            } else {
                return 0;
            }
        }
    }
}
