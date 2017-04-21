using MyLibrary;

namespace CrossMonsters {
    public interface IPlayerDataManager {
        IPlayerStatData PlayerStatData { get; set; }
        IStatInfoData StatInfoData { get; set; }

        void Init( IBasicBackend i_backend );

        int Gold { get; set; }
        int GetStat( string i_key );
    }
}
