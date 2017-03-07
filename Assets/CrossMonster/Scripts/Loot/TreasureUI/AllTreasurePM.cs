using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class AllTreasurePM : PresentationModel, IInitializable {
        public const string VISIBLE_PROPERTY = "IsVisible";

        [Inject]
        ITreasureDataManager TreasureDataManager;

        private List<ITreasureSetPM> mTreasureSetPMs;
        public List<ITreasureSetPM> TreasureSetPMs { get { return mTreasureSetPMs; } set { mTreasureSetPMs = value; } }

        public void Initialize() {
            CreateTreasureSetPMs();
            SetVisibleProperty( false );
        }

        public void Show() {
            SetVisibleProperty( true );
        }

        public void Hide() {
            SetVisibleProperty( false );
        }

        private void CreateTreasureSetPMs() {
            mTreasureSetPMs = new List<ITreasureSetPM>();

            if ( TreasureDataManager.TreasureSetData != null ) {
                foreach ( ITreasureSetData setData in TreasureDataManager.TreasureSetData ) {
                    mTreasureSetPMs.Add( new TreasureSetPM( setData ) );
                }
            }
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
