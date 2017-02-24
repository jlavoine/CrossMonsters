
namespace CrossMonsters {
    public interface IMonsterData  {
        string GetId();

        int GetMaxHP();
        int GetDefense();
        int GetDefenseType();
        int GetDamage();
        int GetDamageType();
        float GetAttackRate();
    }
}
