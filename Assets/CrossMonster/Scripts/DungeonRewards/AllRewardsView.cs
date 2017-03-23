using MyLibrary;
using Zenject;
using UnityEngine;

namespace CrossMonsters {
    public class AllRewardsView : GroupView {
        public GameObject SingleRewardViewPrefab;
        public GameObject SingleRewardViewContent;

        [Inject]
        ISingleRewardPM_Spawner SingleRewardsSpawner;

        [Inject]
        IMessageService Messenger;

        [Inject]
        ICurrentDungeonGameManager DungeonManager;

        private AllRewardsPM mPM;

        void Start() {
            mPM = new AllRewardsPM( SingleRewardsSpawner, Messenger, DungeonManager.Rewards );

            SetModel( mPM.ViewModel );
            CreateSingleRewardViews();
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        private void CreateSingleRewardViews() {
            foreach ( ISingleRewardPM rewardPM in mPM.SingleRewardPMs ) {
                GameObject rewardObject = gameObject.InstantiateUI( SingleRewardViewPrefab, SingleRewardViewContent );
                SingleRewardView viewScript = rewardObject.GetComponent<SingleRewardView>();
                viewScript.Init( rewardPM );
            }
        }
    }
}