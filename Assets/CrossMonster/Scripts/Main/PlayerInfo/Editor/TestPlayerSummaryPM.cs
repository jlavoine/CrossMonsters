using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestPlayerInfoPM : ZenjectUnitTestFixture {

        private IPlayerDataManager MockPlayerManager;
        private ITreasureDataManager MockTreasureManager;
        private IMessageService MockMessenger;

        [SetUp]
        public void CommonInstall() {
            MockPlayerManager = Substitute.For<IPlayerDataManager>();
            MockTreasureManager = Substitute.For<ITreasureDataManager>();
            MockMessenger = Substitute.For<IMessageService>();
        }

        private PlayerSummaryPM CreateSystem() {
            PlayerSummaryPM systemUnderTest = new PlayerSummaryPM( MockPlayerManager, MockTreasureManager, MockMessenger );
            return systemUnderTest;
        }

        [Test]
        public void WhenCreating_Gold_SetToExpectedValue() {
            MockPlayerManager.Gold.Returns( 100 );

            PlayerSummaryPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerSummaryPM.GOLD_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_TreasureLevel_SetToExpectedValue() {
            MockTreasureManager.GetPlayerTreasureLevel().Returns( 11 );

            PlayerSummaryPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "11", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerSummaryPM.TREASURE_LEVEL_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_MessagesSubscribed_AsExpected() {
            PlayerSummaryPM systemUnderTest = CreateSystem();

            MockMessenger.Received().AddListener( GameMessages.PLAYER_GOLD_CHANGED, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_MessagesUnsubscribed_AsExpected() {
            PlayerSummaryPM systemUnderTest = CreateSystem();
            systemUnderTest.Dispose();

            MockMessenger.Received().RemoveListener( GameMessages.PLAYER_GOLD_CHANGED, Arg.Any<Callback>() );
        }
    }
}