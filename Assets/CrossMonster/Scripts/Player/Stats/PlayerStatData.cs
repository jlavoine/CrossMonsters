using System.Collections.Generic;

namespace MonsterMatch {
    public class PlayerStatData : IPlayerStatData {
        public Dictionary<string, PlayerStatEntry> Stats;

        public int GetStatLevel( string i_stat ) {
            if ( Stats.ContainsKey( i_stat ) ) {
                return Stats[i_stat].Level;
            } else {
                return 0;
            }
        }
    }
}