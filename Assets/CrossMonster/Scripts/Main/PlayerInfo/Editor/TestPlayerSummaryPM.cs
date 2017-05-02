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

        [Test]
        public void WhenCreating_GoldSetToExpectedValue() {
            IPlayerDataManager mockManager = Substitute.For<IPlayerDataManager>();
            mockManager.Gold.Returns( 100 );

            PlayerSummaryPM systemUnderTest = new PlayerSummaryPM( mockManager, Substitute.For<IMessageService>() );

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerSummaryPM.GOLD_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_MessagesSubscribed_AsExpected() {
            IMessageService mockMessenger = Substitute.For<IMessageService>();

            PlayerSummaryPM systemUnderTest = new PlayerSummaryPM( Substitute.For<IPlayerDataManager>(), mockMessenger );

            mockMessenger.Received().AddListener( GameMessages.PLAYER_GOLD_CHANGED, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_MessagesUnsubscribed_AsExpected() {
            IMessageService mockMessenger = Substitute.For<IMessageService>();

            PlayerSummaryPM systemUnderTest = new PlayerSummaryPM( Substitute.For<IPlayerDataManager>(), mockMessenger );
            systemUnderTest.Dispose();

            mockMessenger.Received().RemoveListener( GameMessages.PLAYER_GOLD_CHANGED, Arg.Any<Callback>() );
        }
    }
}