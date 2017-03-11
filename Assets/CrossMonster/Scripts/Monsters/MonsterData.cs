using System.Collections.Generic;

namespace CrossMonsters {
    public class MonsterData : IMonsterData {
        public string Id;
        public int MaxHP;
        public int Defense;
        public int DefenseType;
        public int Damage;
        public int DamageType;
        public long AttackRate;
        public List<int> AttackCombo;

        public List<string> Categories;

        public string GetId() { return Id; }
        public int GetMaxHP() { return MaxHP; }
        public int GetDefense() { return Defense; }
        public int GetDefenseType() { return DefenseType; }
        public int GetDamage() { return Damage; }
        public int GetDamageType() { return DamageType; }
        public long GetAttackRate() { return AttackRate; }
        public List<int> GetAttackCombo() { return AttackCombo; }
    }
}
