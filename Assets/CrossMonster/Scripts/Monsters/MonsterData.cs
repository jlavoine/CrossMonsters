
namespace CrossMonsters {
    public class MonsterData : IMonsterData {
        public string Id;
        public int MaxHP;
        public int DefenseType;
        public int Damage;
        public int DamageType;
        public float AttackRate;

        public string GetId() { return Id; }
        public int GetMaxHP() { return MaxHP; }
        public int GetDefenseType() { return DefenseType; }
        public int GetDamage() { return Damage; }
        public int GetDamageType() { return DamageType; }
        public float GetAttackRate() { return AttackRate; }
    }
}
