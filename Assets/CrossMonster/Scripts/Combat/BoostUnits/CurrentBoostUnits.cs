using System.Collections.Generic;

namespace MonsterMatch {
    public class CurrentBoostUnits : ICurrentBoostUnits {
        private List<IBoostUnit> mUnits = new List<IBoostUnit>();
        public List<IBoostUnit> Units { get { return mUnits; } set { mUnits = value; } }

        public int GetEffectValue( string i_effect ) {
            int value = 0;

            foreach ( IBoostUnit unit in Units ) {
                if ( unit.HasEffect( i_effect ) ) {
                    value += unit.GetEffect( i_effect );
                }
            }

            return value;
        }
    }
}
