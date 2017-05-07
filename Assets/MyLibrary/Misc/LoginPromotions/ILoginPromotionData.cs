using System;

namespace MyLibrary {
    public interface ILoginPromotionData  {
        string GetId();
        string GetNameKey();

        DateTime GetStartTime();
        DateTime GetEndTime();

        bool IsActive( DateTime i_time );
    }
}