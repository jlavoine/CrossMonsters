
namespace MonsterMatch {
    public interface IDungeonReward {
        string GetId();
        string GetNameKey();

        LootTypes GetLootType();

        int GetCount();

        void Award();
    }
}
