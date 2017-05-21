using System;
using System.Collections.Generic;

namespace MyLibrary {
    public class LoginPromotionData : ILoginPromotionData {
        public string Id;
        public string PromoPrefab;
        public long StartDateInMs;
        public long EndDateInMs;
        public List<GameRewardData> RewardData;

        public string GetId() {
            return Id;
        }

        public string GetNameKey() {
            return GetId() + "_Name";
        }

        public string GetPromoPrefab() {
            return PromoPrefab;
        }

        public int GetRewardsCount() {
            return RewardData.Count;
        }

        public List<IGameRewardData> GetRewardData() {
            List<IGameRewardData> rewards = new List<IGameRewardData>();
            foreach ( GameRewardData data in RewardData ) {
                rewards.Add( data );
            }

            return rewards;
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
