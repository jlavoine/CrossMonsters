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
    }
}
