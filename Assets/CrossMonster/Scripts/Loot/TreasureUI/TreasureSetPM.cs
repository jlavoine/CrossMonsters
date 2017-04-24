using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class TreasureSetPM : PresentationModel, ITreasureSetPM {
        public const string NAME_PROPERTY = "TreasureSetName";

        readonly ITreasurePM_Spawner TreasurePM_Spawner;
        readonly IStringTableManager StringTableManager;

        private ITreasureSetData mData;

        private List<ITreasurePM> mTreasurePMs;
        public List<ITreasurePM> TreasurePMs { get { return mTreasurePMs; } set { mTreasurePMs = value; } }

        public TreasureSetPM( ITreasureSetData i_data, ITreasurePM_Spawner i_spawner, IStringTableManager i_stringTableManager ) {
            StringTableManager = i_stringTableManager;
            TreasurePM_Spawner = i_spawner;
            mData = i_data;

            SetNameProperty();
            CreateTreasurePMs();
        }

        private void SetNameProperty() {
            string text = StringTableManager.Get( mData.GetId() + "_Name" );
            ViewModel.SetProperty( NAME_PROPERTY, text );
        }

        private void CreateTreasurePMs() {
            TreasurePMs = new List<ITreasurePM>();

            if ( mData.GetTreasuresInSet() != null ) {
                foreach ( string treasureId in mData.GetTreasuresInSet() ) {
                    TreasurePMs.Add( TreasurePM_Spawner.Create( treasureId ) );
                }
            }
        }

        public class Factory : Factory<ITreasureSetData, TreasureSetPM> { }
    }
}