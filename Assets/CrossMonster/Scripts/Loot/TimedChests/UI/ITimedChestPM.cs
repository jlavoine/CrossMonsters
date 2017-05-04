using MyLibrary;

namespace MonsterMatch {
    public interface ITimedChestPM : IPresentationModel {
        void Open();
        void UpdateProperties();
        void ShowOpenReward( IDungeonReward i_reward );
    }
}
