using MyLibrary;
using UnityEngine;
using Zenject;

namespace CrossMonsters {
    public class TreasureSetView : GroupView {
        [Inject]
        DiContainer container;

        public GameObject TreasureViewPrefab;
        public GameObject TreasureViewContent;

        private ITreasureSetPM mPM;

        public void Init( ITreasureSetPM i_pm ) {
            mPM = i_pm;

            SetModel( i_pm.ViewModel );
            CreateTreasureViews();
        }

        private void CreateTreasureViews() {
            foreach ( ITreasurePM pm in mPM.TreasurePMs ) {
                GameObject treasureObject = container.InstantiatePrefab( TreasureViewPrefab, TreasureViewContent.transform );
                TreasureView treasureView = treasureObject.GetComponent<TreasureView>();
                treasureView.Init( pm );
            }
        }
    }
}
