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
    public class TestGameManager : ZenjectUnitTestFixture {
        [Inject]
        IMessageService MyMessenger;

        [Inject]
        ICurrentDungeonGameManager MockCurrentDungeon;

        [Inject]
        IBackendManager MockBackend;

        [Inject]
        IDungeonWavePM MockWavePM;

        [Inject]
        IGamePlayer MockPlayer;

        [Inject]
        IAudioManager MockAudio;

        [Inject]
        GameManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IDungeonWavePM>().FromInstance( Substitute.For<IDungeonWavePM>() );
            Container.Bind<IBackendManager>().FromInstance( Substitute.For<IBackendManager>() );
            Container.Bind<IGamePlayer>().FromInstance( Substitute.For<IGamePlayer>() );
            Container.Bind<IAudioManager>().FromInstance( Substitute.For<IAudioManager>() );
            Container.Bind<ICurrentDungeonGameManager>().FromInstance( Substitute.For<ICurrentDungeonGameManager>() );
            Container.Bind<GameManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            systemUnderTest.Initialize();

            MyMessenger.Received().AddListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            systemUnderTest.Dispose();

            MyMessenger.Received().RemoveListener( GameMessages.PLAYER_DEAD, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreated_StateIsPaused() {
            Assert.AreEqual( GameStates.Paused, systemUnderTest.State );
        }

        [Test]
        public void WhenPlayerDies_GameStateIsEnded() {
            systemUnderTest.OnPlayerDied();

            Assert.AreEqual( GameStates.Ended, systemUnderTest.State );
        }

        [Test]
        public void WhenPlayerDies_SoundIsPlayed() {
            systemUnderTest.OnPlayerDied();

            MockAudio.Received().PlayOneShot( CombatAudioKeys.GAME_OVER_LOSS );
        }

        [Test]
        public void WhenAllMonstersDead_GameStateIsEnded() {
            systemUnderTest.OnAllMonstersDead();

            Assert.AreEqual( GameStates.Ended, systemUnderTest.State );
        }
    
        [Test]
        public void WhenAllMonstersDead_DungeonRewardsAreAwarded() {
            systemUnderTest.OnAllMonstersDead();

            MockCurrentDungeon.Received().AwardRewards();
        }

        [Test]
        public void WhenAllMonstersDead_SoundIsPlayed() {
            systemUnderTest.OnAllMonstersDead();

            MockAudio.Received().PlayOneShot( CombatAudioKeys.GAME_OVER_WIN );
        }

        [Test]
        public void WhenAllMonstersDead_BackendVictoryMethodCalled() {
            systemUnderTest.OnAllMonstersDead();

            MockBackend.Received().MakeCloudCall( BackendMethods.COMPLETE_DUNGEON_SESSION, Arg.Any<Dictionary<string, string>>(), Arg.Any<Callback<Dictionary<string, string>>>() );
        }

        [Test]
        public void WhenPlayerDies_NoBackendVictoryMethodCalled() {
            systemUnderTest.OnPlayerDied();

            MockBackend.DidNotReceive().MakeCloudCall( BackendMethods.COMPLETE_DUNGEON_SESSION, Arg.Any<Dictionary<string, string>>(), Arg.Any<Callback<Dictionary<string, string>>>() );
        }

        [Test]
        public void WhenPlayerDies_NoDungeonRewardsAwarded() {
            systemUnderTest.OnPlayerDied();

            MockCurrentDungeon.DidNotReceive().AwardRewards();
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

        [Test]
        public void WhenPreparingForNextWave_GameIsPaused() {
            systemUnderTest.PrepareForNextWave();

            Assert.AreEqual( GameStates.Paused, systemUnderTest.State );
        }

        [Test]
        public void WhenPreparingForNextWave_WavePM_IsShown() {
            systemUnderTest.PrepareForNextWave();

            MockWavePM.Received().Show();
        }

        [Test]
        public void WhenPreparingForNextWave_GamePlayerIsAltered() {
            systemUnderTest.PrepareForNextWave();

            MockPlayer.Received().OnWaveFinished();
        }
    }
}
