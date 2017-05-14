using MyLibrary;
using UnityEngine;

namespace MonsterMatch {
    public class SingleLoginPromoDisplayView : GroupView {        
        private ISingleLoginPromoDisplayPM mPM;

        public GameObject SingleRewardPrefab;
        public GameObject RewardArea;

        public void Init( ISingleLoginPromoDisplayPM i_pm ) {
            mPM = i_pm;

            SetModel( mPM.ViewModel );

            CreateRewardDisplays();
        }

        private void CreateRewardDisplays() {
            foreach ( ISingleLoginPromoRewardPM pm in mPM.RewardPMs ) {
                GameObject displayObject = gameObject.InstantiateUI( SingleRewardPrefab, RewardArea );
                SingleLoginPromoRewardView viewScript = displayObject.GetComponent<SingleLoginPromoRewardView>();
                viewScript.Init( pm );
            }
        }

        public void ClosePromo() {
            mPM.Hide();
        }
    }
}
