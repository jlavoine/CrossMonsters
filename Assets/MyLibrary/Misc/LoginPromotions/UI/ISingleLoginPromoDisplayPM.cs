using System.Collections.Generic;

namespace MyLibrary {
    public interface ISingleLoginPromoDisplayPM : IBasicWindowPM {
        string GetPrefab();
        string GetId();

        void UpdateVisibilityBasedOnCurrentlyDisplayedPromo( string i_id );

        List<ISingleLoginPromoRewardPM> RewardPMs { get; }
    }
}
