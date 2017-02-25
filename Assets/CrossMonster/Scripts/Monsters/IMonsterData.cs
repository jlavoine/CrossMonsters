using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterData  {
        string GetId();

        int GetMaxHP();
        int GetDefense();
        int GetDefenseType();
        int GetDamage();
        int GetDamageType();
        long GetAttackRate();
        List<int> GetAttackCombo();
    }
}
