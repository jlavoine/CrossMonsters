using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameManager : ZenjectUnitTestFixture {
        [Inject]
        IMessageService MyMessenger;

        [Inject]
        GameManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<GameManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            systemUnderTest.Initialize();

            MyMessenger.Received().AddListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
            MyMessenger.Received().AddListener( GameMessages.ALL_MONSTERS_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            systemUnderTest.Dispose();

            MyMessenger.Received().RemoveListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
            MyMessenger.Received().RemoveListener( GameMessages.ALL_MONSTERS_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreated_StateIsPlaying() {
            Assert.AreEqual( GameStates.Playing, systemUnderTest.State );
        }

        [Test]
        public void WhenPlayerDies_GameStateIsEnded() {
            systemUnderTest.OnPlayerDied();

            Assert.AreEqual( GameStates.Ended, systemUnderTest.State );
        }

        [Test]
        public void WhenAllMonstersDead_GameStateIsEnded() {
            systemUnderTest.OnAllMonstersDead();

            Assert.AreEqual( GameStates.Ended, systemUnderTest.State );
        }

        [Test]
        public void WhenPlayerDies_GameOverMessageSent() {
            systemUnderTest.OnPlayerDied();

            MyMessenger.Received().Send<bool>( GameMessages.GAME_OVER, false );
        }

        [Test]
        public void WhenAllMonstersDead_GameOverMessageSent() {
            systemUnderTest.OnAllMonstersDead();

            MyMessenger.Received().Send<bool>( GameMessages.GAME_OVER, true );
        }
    }
}
