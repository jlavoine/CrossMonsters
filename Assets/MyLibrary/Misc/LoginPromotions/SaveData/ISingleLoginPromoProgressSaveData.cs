
namespace MyLibrary {
    public interface ISingleLoginPromoProgressSaveData {
        string GetId();
        long GetLastCollectedTime();
        int GetCollectCount();
    }
}