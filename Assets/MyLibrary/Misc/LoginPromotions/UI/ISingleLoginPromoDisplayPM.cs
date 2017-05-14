using System.Collections.Generic;

namespace MyLibrary {
    public interface ISingleLoginPromoDisplayPM : IBasicWindowPM {
        string GetPrefab();

        void UpdateVisibilityBasedOnCurrentlyDisplayedPromo( string i_id );

        List<ISingleLoginPromoRewardPM> RewardPMs { get; }
    }
}
