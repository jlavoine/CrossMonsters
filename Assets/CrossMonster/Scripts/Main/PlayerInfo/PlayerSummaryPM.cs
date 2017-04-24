using MyLibrary;

namespace MonsterMatch {
    public class PlayerSummaryPM : PresentationModel, IPlayerSummaryPM {
        public const string GOLD_PROPERTY = "Gold";

        readonly IPlayerDataManager mManager;

        public PlayerSummaryPM( IPlayerDataManager i_manager ) {
            mManager = i_manager;

            SetGoldProperty();
        }

        private void SetGoldProperty() {
            ViewModel.SetProperty( GOLD_PROPERTY, mManager.Gold.ToString() );
        }
    }
}
