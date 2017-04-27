using MyLibrary;
using Zenject;
using UnityEngine;

namespace MonsterMatch {
    public class TimedChestsMainView : GroupView {
        [Inject]
        ITimedChestsMainPM PM;

        public GameObject TimedChestViewPrefab;
        public GameObject TimedChestsContent;

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
            foreach ( ITimedChestPM rewardPM in PM.ChestPMs ) {
                GameObject rewardObject = gameObject.InstantiateUI( TimedChestViewPrefab, TimedChestsContent );
                TimedChestView viewScript = rewardObject.GetComponent<TimedChestView>();
                viewScript.Init( rewardPM );
            }
        }
    }
}