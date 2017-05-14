
namespace MyLibrary {
    public interface IGameReward {
        string GetId();
        string GetNameKey();

        int GetCount();

        void Award();
    }
}
