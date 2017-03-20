
namespace CrossMonsters {
    public interface IDungeonRewardData {
        string GetId();
        string GetNameKey();

        LootTypes GetLootType();

        int GetCount();        
    }
}