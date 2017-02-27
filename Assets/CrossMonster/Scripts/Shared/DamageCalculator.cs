using UnityEngine;

namespace CrossMonsters {
    public class DamageCalculator : IDamageCalculator {
        private static IDamageCalculator mInstance;
        public static IDamageCalculator Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new DamageCalculator();
                }
                return mInstance;
            }
            set {
                // tests only!
                mInstance = value;
            }
        }

        public int GetDamageFromMonster( IGameMonster i_monster, IGamePlayer i_player ) {
            int monsterAttack = i_monster.AttackPower;
            int playerDefense = i_player.GetDefenseForType( i_monster.AttackType );

            return Mathf.Max( 1, monsterAttack - playerDefense );
        }
    }
}