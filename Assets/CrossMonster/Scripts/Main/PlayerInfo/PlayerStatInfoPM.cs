using MyLibrary;

namespace MonsterMatch {
    public class PlayerStatInfoPM : BasicWindowPM, IPlayerStatInfoPM {
        public const string HP_PROPERTY = "HP";
        public const string ATK_PROPERTY = "Atk";
        public const string DEF_PROPERTY = "Def";

        readonly IPlayerDataManager mPlayerDataManager;

        public PlayerStatInfoPM( IPlayerDataManager i_manager ) {
            mPlayerDataManager = i_manager;

            SetVisibleProperty( false );
            SetStatValues();
        }

        private void SetStatValues() {
            ViewModel.SetProperty( HP_PROPERTY, mPlayerDataManager.GetStat( PlayerStats.HP ).ToString() );
            ViewModel.SetProperty( ATK_PROPERTY, mPlayerDataManager.GetStat( PlayerStats.PHY_ATK ).ToString() );
            ViewModel.SetProperty( DEF_PROPERTY, mPlayerDataManager.GetStat( PlayerStats.PHY_DEF ).ToString() );
        }
    }
}