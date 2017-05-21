using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;
using System;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestLoginPromoPopupHelper : ZenjectUnitTestFixture {
        private ILoginPromotionManager MockPromoManager;
        private ILoginPromoDisplaysPM MockAllPromosPM;
        private IBackendManager MockBackend;

        [SetUp]
        public void CommonInstall() {
            MockPromoManager = Substitute.For<ILoginPromotionManager>();
            MockAllPromosPM = Substitute.For<ILoginPromoDisplaysPM>();
            MockBackend = Substitute.For<IBackendManager>();
        }

        static object[] ShouldShowPromoTests = {
            new object[] { true, true, true, false },
            new object[] { true, true, false, true },
            new object[] { true, false, true, false },
            new object[] { true, false, false, false },
            new object[] { false, true, true, false },
            new object[] { false, true, false, false },
            new object[] { false, false, true, false },
            new object[] { false, false, false, false },
        };

        [Test, TestCaseSource( "ShouldShowPromoTests" )]
        public void ShouldShowPromo_ReturnsAsExpected( bool i_doesHaveUI, bool i_hasRewardsRemaining, bool i_hasClaimedRewardToday, bool i_expected ) {
            ISingleLoginPromoProgressSaveData mockData = Substitute.For<ISingleLoginPromoProgressSaveData>();
            LoginPromoPopupHelper systemUnderTest = CreateSystem();
            MockAllPromosPM.DoesHaveDisplayForPromo( Arg.Any<string>() ).Returns( i_doesHaveUI );
            mockData.HasRewardBeenClaimedToday( MockBackend ).Returns( i_hasClaimedRewardToday );
            mockData.AreRewardsRemaining( Arg.Any<ILoginPromotionData>() ).Returns( i_hasRewardsRemaining );

            bool shouldShow = systemUnderTest.ShouldShowPromoAsPopup( mockData );

            Assert.AreEqual( i_expected, shouldShow );
        }


        private LoginPromoPopupHelper CreateSystem() {
            LoginPromoPopupHelper systemUnderTest = new LoginPromoPopupHelper( MockBackend, MockPromoManager, MockAllPromosPM );
            return systemUnderTest;
        }
    }
}
