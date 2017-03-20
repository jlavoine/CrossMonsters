using MyLibrary;

namespace CrossMonsters {
    public class PlayerInfoPM : PresentationModel, IPlayerInfoPM {
        public const string GOLD_PROPERTY = "Gold";

        readonly IPlayerDataManager mManager;

        public PlayerInfoPM( IPlayerDataManager i_manager ) {
            mManager = i_manager;

            SetGoldProperty();
        }

        private void SetGoldProperty() {
            ViewModel.SetProperty( GOLD_PROPERTY, mManager.Gold.ToString() );
        }
    }
}
