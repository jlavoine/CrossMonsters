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
    public class TestMonsterManager : ZenjectUnitTestFixture {
        [Inject]
        IGameRules GameRules;

        [Inject]
        IGameBoard GameBoard;

        [Inject]
        IMessageService MessageService;

        [Inject]
        IGameManager GameManager;

        [Inject]
        IAudioManager Audio;

        [Inject]
        MonsterManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameBoard>().FromInstance( Substitute.For<IGameBoard>() );
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IGameManager>().FromInstance( Substitute.For<IGameManager>() );
            Container.Bind<IAudioManager>().FromInstance( Substitute.For<IAudioManager>() );
            Container.Bind<MonsterManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenManagerTicks_CurrentWaveTicks() {
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            systemUnderTest.CurrentWave = mockWave;

            systemUnderTest.Tick( 1 );

            mockWave.Received().Tick( 1 );
        }

        [Test]
        public void DoesMoveMatchAnyCurrentMonsters_ReturnsCurrentWaveValue() {
            List<IGamePiece> mockMove = new List<IGamePiece>();
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            systemUnderTest.CurrentWave = mockWave;

            systemUnderTest.DoesMoveMatchAnyCurrentMonsters( mockMove );

            mockWave.Received().DoesMoveMatchAnyCurrentMonsters( mockMove );
        }

        [Test]
        public void WhenProcessingPlayerMove_CurrentWaveIsProcessed() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<IGamePiece> mockMove = new List<IGamePiece>();
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            systemUnderTest.CurrentWave = mockWave;

            systemUnderTest.ProcessPlayerMoveOnCurrentWave( mockPlayer, mockMove );

            mockWave.Received().ProcessPlayerMove( mockPlayer, mockMove );
        }

        [Test]
        public void AfterProcessingMove_CurrentWaveIsCheckedToBeClear() {
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            systemUnderTest.CurrentWave = mockWave;
            systemUnderTest.ProcessPlayerMove( Substitute.For<IGamePlayer>(), new List<IGamePiece>() );

            mockWave.Received().IsCleared();
        }

        [Test]
        public void AfterProcessingPlayerMove_IfAllWavesAreClear_GameManagerIsAlerted() {
            SetCurrentWaveToBeClear();
            systemUnderTest.RemainingWaves = new List<IMonsterWave>();

            systemUnderTest.ProcessPlayerMove( Substitute.For<IGamePlayer>(), new List<IGamePiece>() );

            GameManager.Received().OnAllMonstersDead();
        }

        [Test]
        public void AfterProcessingPlayerMove_IfRemainingWave_WaveIsMadeCurrentAndPrepared() {
            SetCurrentWaveToBeClear();
            IMonsterWave mockNextWave = Substitute.For<IMonsterWave>();
            systemUnderTest.RemainingWaves = new List<IMonsterWave>() { mockNextWave };

            systemUnderTest.ProcessPlayerMove( Substitute.For<IGamePlayer>(), new List<IGamePiece>() );

            Assert.AreEqual( mockNextWave, systemUnderTest.CurrentWave );
            mockNextWave.Received().Prepare();
            Audio.Received().PlayOneShot( CombatAudioKeys.START_NEXT_WAVE );
        }

        [Test]
        public void AfterProcessingPlayerMove_IfRemainingWave_GameManagerGetsPrepared() {
            SetCurrentWaveToBeClear();
            IMonsterWave mockNextWave = Substitute.For<IMonsterWave>();
            systemUnderTest.RemainingWaves = new List<IMonsterWave>() { mockNextWave };

            systemUnderTest.ProcessPlayerMove( Substitute.For<IGamePlayer>(), new List<IGamePiece>() );

            GameManager.Received().PrepareForNextWave();
        }

        [Test]
        public void AfterProcessingPlayerMove_IfRemainingWave_NewWaveEventIsSent() {
            SetCurrentWaveToBeClear();
            IMonsterWave mockNextWave = Substitute.For<IMonsterWave>();
            systemUnderTest.RemainingWaves = new List<IMonsterWave>() { mockNextWave };

            systemUnderTest.ProcessPlayerMove( Substitute.For<IGamePlayer>(), new List<IGamePiece>() );

            MessageService.Received().Send( GameMessages.NEW_MONSTER_WAVE_EVENT );
        }

        [Test]
        public void WhenSettingMonsters_ChecksToSeeIfBoardShouldBeRandomized() {
            systemUnderTest.SetMonsters( new List<IMonsterWaveData>() );

            GameBoard.Received().RandomizeGameBoardIfNoMonsterCombosAvailable();
        }

        [Test]
        public void GetLongestComboFromCurrentWave_ReturnsExpectedValue() {
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            mockWave.GetLongestCombo().Returns( 11 );
            systemUnderTest.CurrentWave = mockWave;

            Assert.AreEqual( 11, systemUnderTest.GetLongestComboFromCurrentWave() );
        }

        private void SetCurrentWaveToBeClear() {
            IMonsterWave mockWave = Substitute.For<IMonsterWave>();
            mockWave.IsCleared().Returns( true );
            systemUnderTest.CurrentWave = mockWave;
        }
    }
}