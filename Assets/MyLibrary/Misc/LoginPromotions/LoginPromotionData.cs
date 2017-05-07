using System;

namespace MyLibrary {
    public class LoginPromotionData : ILoginPromotionData {
        public string Id;
        public long StartDateInMs;
        public long EndDateInMs;        

        public string GetId() {
            return Id;
        }

        public string GetNameKey() {
            return GetId() + "_Name";
        }

        public DateTime GetStartTime() {
            return new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( StartDateInMs );
        }

        public DateTime GetEndTime() {
            return new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( EndDateInMs );
        }

        public bool IsActive( DateTime i_time ) {
            return i_time >= GetStartTime() && i_time <= GetEndTime();
        }
    }
}
