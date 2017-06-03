using System.Collections.Generic;

namespace MonsterMatch {
    public class ExpeditionUnitCustomData : IExpeditionUnitCustomData {
        public Dictionary<string, int> Effects;

        public bool HasEffect( string i_effectId ) {
            return Effects.ContainsKey( i_effectId );
        }

        public int GetEffect( string i_effectId ) {
            if ( HasEffect( i_effectId ) ) {
                return Effects[i_effectId];
            } else {
                return 0;
            }
        }
    }
}