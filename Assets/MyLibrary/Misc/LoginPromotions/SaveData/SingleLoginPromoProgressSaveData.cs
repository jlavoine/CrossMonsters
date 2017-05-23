using System;

namespace MyLibrary {
    public class SingleLoginPromoProgressSaveData : ISingleLoginPromoProgressSaveData {
        public string Id;
        public long LastCollectedTime;
        public int CollectCount;

        public string GetId() {
            return Id;
        }

        public long GetLastCollectedTime() {
            return LastCollectedTime;
        }

        public int GetCollectCount() {
            return CollectCount;
        }

        public void OnAwarded( long i_curTime ) {
            LastCollectedTime = i_curTime;
            CollectCount++;
        }

        public bool AreRewardsRemaining( ILoginPromotionData i_promoData ) {
            if ( i_promoData != null ) {
                return CollectCount < i_promoData.GetRewardsCount();
            } else {
                return false;
            }
        }

        public bool HasRewardBeenClaimedToday( IBackendManager i_backend ) {
            int curDay = i_backend.GetBackend<IBasicBackend>().GetDateTime().DayOfYear;
            int curYear = i_backend.GetBackend<IBasicBackend>().GetDateTime().Year;
            int lastCollectedDay = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( LastCollectedTime ).DayOfYear;
            int lastCollectedYear = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( LastCollectedTime ).Year;

            return lastCollectedYear == curYear && lastCollectedDay >= curDay;
        }
    }
}
