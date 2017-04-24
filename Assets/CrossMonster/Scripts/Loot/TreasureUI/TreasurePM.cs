using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class TreasurePM : PresentationModel, ITreasurePM {
        public const string VISIBLE_PROPERTY = "IsVisible";

        readonly ITreasureDataManager TreasureDataManager;

        public TreasurePM( ITreasureDataManager treasureDataManager, string i_treasureId ) {
            TreasureDataManager = treasureDataManager;

            bool hasTreasure = TreasureDataManager.DoesPlayerHaveTreasure( i_treasureId );
            SetVisibleProperty( hasTreasure );
        }

        private void SetVisibleProperty( bool i_vis ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_vis );
        }

        public class Factory : Factory<string, TreasurePM> { }
    }
}
