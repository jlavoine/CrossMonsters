
namespace CrossMonsters {
    public interface IDungeonRewardData {
        string GetId();

        LootTypes GetLootType();

        int GetCount();        
    }
}