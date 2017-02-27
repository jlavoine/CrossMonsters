using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePlayerPM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePlayerPM systemUnderTest = new GamePlayerPM( Substitute.For<IGamePlayer>() );

            MyMessenger.Instance.Received().AddListener( GameMessages.UPDATE_PLAYER_HP, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePlayerPM systemUnderTest = new GamePlayerPM( Substitute.For<IGamePlayer>() );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( GameMessages.UPDATE_PLAYER_HP, Arg.Any<Callback>() );
        }

        [Test]
        public void HpProperty_MatchesGamePlayerHP() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.HP.Returns( 100 );
            GamePlayerPM systemUnderTest = new GamePlayerPM( mockPlayer );

            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( GamePlayerPM.HP_PROPERTY ) );
        }        
    }
}
