
namespace MonsterMatch {
    public interface IDungeonRewardData {
        string GetId();

        LootTypes GetLootType();

        int GetCount();        
    }
}