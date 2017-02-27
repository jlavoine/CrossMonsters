using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameManager : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GameManager systemUnderTest = new GameManager();

            MyMessenger.Instance.Received().AddListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GameManager systemUnderTest = new GameManager();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreated_StateIsPlaying() {
            GameManager systemUnderTest = new GameManager();

            Assert.AreEqual( GameStates.Playing, systemUnderTest.State );
        }

        [Test]
        public void WhenPlayerDies_GameStateIsEnded() {
            GameManager systemUnderTest = new GameManager();

            systemUnderTest.OnPlayerDied();

            Assert.AreEqual( GameStates.Ended, systemUnderTest.State );
        }
    }
}
