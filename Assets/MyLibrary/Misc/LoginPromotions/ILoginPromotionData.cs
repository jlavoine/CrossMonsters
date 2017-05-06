using System;

namespace MyLibrary {
    public interface ILoginPromotionData  {
        string GetId();
        DateTime GetStartTime();
        DateTime GetEndTime();
        bool IsActive( DateTime i_time );
    }
}