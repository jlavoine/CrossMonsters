using System;
using System.Collections.Generic;

namespace MyLibrary {
    public interface ILoginPromotionData  {
        string GetId();
        string GetNameKey();
        string GetPromoPrefab();

        int GetRewardsCount();

        List<IGameRewardData> GetRewardData();

        DateTime GetStartTime();
        DateTime GetEndTime();

        bool IsActive( DateTime i_time );
    }
}