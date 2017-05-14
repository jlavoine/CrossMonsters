using MyLibrary;

namespace MonsterMatch {
    public interface IDungeonReward : IGameReward {
        LootTypes GetLootType();
    }
}
