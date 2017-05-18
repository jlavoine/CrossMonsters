using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestSingleLoginPromoDisplayPM : ZenjectUnitTestFixture {
        private IStringTableManager MockStringTable;
        private ILoginPromotionData MockData;
        private ISingleLoginPromoRewardPM_Spawner MockRewardSpawner;
        private IMessageService MockMessenger;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockData = Substitute.For<ILoginPromotionData>();
            MockRewardSpawner = Substitute.For<ISingleLoginPromoRewardPM_Spawner>();
            MockMessenger = Substitute.For<IMessageService>();
        }

        [Test]
        public void GetMethodsForData_ReturnAsExpected() {
            MockData.GetPromoPrefab().Returns( "TestPrefab" );
            MockData.GetId().Returns( "TestId" );

            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "TestPrefab", systemUnderTest.GetPrefab() );
            Assert.AreEqual( "TestId", systemUnderTest.GetId() );
        }

        [Test]
        public void WhenCreating_TitleSetAsExpected() {
            MockData.GetNameKey().Returns( "TestId" );
            MockStringTable.Get( "TestId" ).Returns( "TestTitle" );

            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "TestTitle", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleLoginPromoDisplayPM.TITLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_DateAvailableString_SetAsExpected() {
            MockData.GetStartTime().Returns( new DateTime( 0 ) );
            MockData.GetEndTime().Returns( new DateTime( 1000 ) );
            string expected = string.Format( SingleLoginPromoDisplayPM.DATE_AVAILABLE_FORMAT, MockData.GetStartTime().ToString(), MockData.GetEndTime().ToString() );

            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();

            Assert.AreEqual( expected, systemUnderTest.ViewModel.GetPropertyValue<string>( SingleLoginPromoDisplayPM.DATE_AVAILABLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_RewardPMs_AreCreated() {
            MockData.GetRewardData().Returns( new List<IGameRewardData>() { Substitute.For<IGameRewardData>(), Substitute.For<IGameRewardData>() } );

            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();

            Assert.AreEqual( 2, systemUnderTest.RewardPMs.Count );
        }

        [Test]
        public void WhenUpdatingVisibility_IfIdMatches_PromoIsVisible() {
            MockData.GetId().Returns( "TestId" );
            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( SingleLoginPromoDisplayPM.VISIBLE_PROPERTY, false );

            systemUnderTest.UpdateVisibilityBasedOnCurrentlyDisplayedPromo( "TestId" );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleLoginPromoDisplayPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenUpdatingVisibility_IfIdDoesNotMatch_PromoIsNotVisible() {
            MockData.GetId().Returns( "TestId" );
            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( SingleLoginPromoDisplayPM.VISIBLE_PROPERTY, true );

            systemUnderTest.UpdateVisibilityBasedOnCurrentlyDisplayedPromo( "NonMatchingId" );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleLoginPromoDisplayPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenDismissed_EventIsSent() {
            SingleLoginPromoDisplayPM systemUnderTest = CreateSystem();

            systemUnderTest.Hide();

            MockMessenger.Received().Send( SingleLoginPromoDisplayPM.PROMO_DISMISSED_EVENT );
        }

        private SingleLoginPromoDisplayPM CreateSystem() {
            SingleLoginPromoDisplayPM systemUnderTest = new SingleLoginPromoDisplayPM( MockMessenger, MockRewardSpawner, MockStringTable, MockData );
            return systemUnderTest;
        }
    }
}
