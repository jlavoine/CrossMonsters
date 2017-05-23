using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    [TestFixture]
    public class TestSingleLoginPromoSaveData : ZenjectUnitTestFixture {

        static object[] AreRewardsRemainingTests = {
            new object[] { 0, 3, true },
            new object[] { 1, 3, true },
            new object[] { 2, 3, true },
            new object[] { 3, 3, false },
            new object[] { 4, 3, false }
        };

        [Test, TestCaseSource( "AreRewardsRemainingTests" )]
        public void AreRewardsRemaining_ReturnsAsExpected( int i_rewardsCollected, int i_totalRewards, bool i_expectedResult ) {
            SingleLoginPromoProgressSaveData systemUnderTest = new SingleLoginPromoProgressSaveData();
            systemUnderTest.CollectCount = i_rewardsCollected;

            ILoginPromotionData mockData = Substitute.For<ILoginPromotionData>();
            mockData.GetRewardsCount().Returns( i_totalRewards );
            bool areRewardsRemaining = systemUnderTest.AreRewardsRemaining( mockData );

            Assert.AreEqual( i_expectedResult, areRewardsRemaining );
        }

        [Test]
        public void WhenPromoDataIsNull_AreRewardsRemaining_ReturnsFalse() {
            SingleLoginPromoProgressSaveData systemUnderTest = new SingleLoginPromoProgressSaveData();
            systemUnderTest.CollectCount = 1;

            bool areRewardsRemaining = systemUnderTest.AreRewardsRemaining( null );

            Assert.IsFalse( areRewardsRemaining );
        }

        static object[] HasRewardBeenClaimedTodayTests = {
            new object[] { new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ), false },
            new object[] { new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), new DateTime( 1970, 1, 31, 0, 0, 0, DateTimeKind.Utc ), false },
            new object[] { new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), true },
            new object[] { new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), new DateTime( 1970, 2, 2, 0, 0, 0, DateTimeKind.Utc ), true },
            new object[] { new DateTime( 1971, 2, 1, 0, 0, 0, DateTimeKind.Utc ), new DateTime( 1970, 2, 1, 0, 0, 0, DateTimeKind.Utc ), false },
        };

        [Test, TestCaseSource( "HasRewardBeenClaimedTodayTests" )]
        public void IsNoChain_ReturnsAsExpected( DateTime i_curTime, DateTime i_lastCollectedTime, bool i_expected ) {
            IBackendManager mockBackendManager = Substitute.For<IBackendManager>();
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            mockBackend.GetDateTime().Returns( i_curTime );
            mockBackendManager.GetBackend<IBasicBackend>().Returns( mockBackend );

            SingleLoginPromoProgressSaveData systemUnderTest = new SingleLoginPromoProgressSaveData();
            systemUnderTest.LastCollectedTime = (long)i_lastCollectedTime.ToUniversalTime().Subtract( new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ) ).TotalMilliseconds;

            bool hasClaimedToday = systemUnderTest.HasRewardBeenClaimedToday( mockBackendManager );

            Assert.AreEqual( i_expected, hasClaimedToday );
        }

        [Test]
        public void WhenAwarded_CollectTimeIsSet() {
            SingleLoginPromoProgressSaveData systemUnderTest = new SingleLoginPromoProgressSaveData();
            systemUnderTest.LastCollectedTime = 0;

            systemUnderTest.OnAwarded( 1000 );

            Assert.AreEqual( 1000, systemUnderTest.GetLastCollectedTime() );
        }

        [Test]
        public void WhenAwarded_CollectCountIsIncremented() {
            SingleLoginPromoProgressSaveData systemUnderTest = new SingleLoginPromoProgressSaveData();
            systemUnderTest.CollectCount = 1;

            systemUnderTest.OnAwarded( 1000 );

            Assert.AreEqual( 2, systemUnderTest.GetCollectCount() );
        }
    }
}
