using MyLibrary;
using Zenject;
using UnityEngine;

namespace CrossMonsters {
    public class AllTreasureView : GroupView {
        [Inject]
        AllTreasurePM PM;

        [Inject]
        DiContainer container;

        public GameObject TreasureSetViewPrefab;
        public GameObject TreasureSetsContent;

        void Start() {
            SetModel( PM.ViewModel );
            CreateTreasureSetViews();
        }

        public void ShowView() {
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }

        private void CreateTreasureSetViews() {
            foreach ( ITreasureSetPM pm in PM.TreasureSetPMs ) {
                GameObject setObject = container.InstantiatePrefab( TreasureSetViewPrefab, TreasureSetsContent.transform );
                TreasureSetView setView = setObject.GetComponent<TreasureSetView>();
                setView.Init( pm );
            }
        }
    }
}