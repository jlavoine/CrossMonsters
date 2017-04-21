using System.Collections.Generic;

namespace CrossMonsters {
    public class StatInfoData : IStatInfoData {
        public Dictionary<string, StatInfoEntry> Stats;

        public float GetValuePerLevel( string i_stat ) {
            if ( Stats.ContainsKey( i_stat ) ) {
                return Stats[i_stat].ValuePerLevel;
            } else {
                return 0f;
            }
        }
    }
}