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
    public class TestMonsterManager : ZenjectUnitTestFixture {
        [Inject]
        IGameRules GameRules;

        [Inject]
        IGameManager GameManager;

        [Inject]
        MonsterManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.Bind<IGameManager>().FromInstance( Substitute.For<IGameManager>() );
            Container.Bind<MonsterManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenManagerTicks_AllMonstersTick() {
            List<IGameMonster> mockMonsters = new List<IGameMonster>();
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.CurrentMonsters = mockMonsters;
            UnityEngine.Debug.LogError( "ticking" );
            systemUnderTest.Tick( 1000 );

            foreach ( IGameMonster monster in systemUnderTest.CurrentMonsters ) {
                monster.Received().Tick( 1000 );
            }
        }

        [Test]
        public void DoesMoveMatchAnyCurrentMonsters_ReturnsTrue_WhenMonsterMatches() {
            List<IGamePiece> mockMove = new List<IGamePiece>();
            List<IGameMonster> mockCurrentMonsters = new List<IGameMonster>();
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( true ) );
            systemUnderTest.CurrentMonsters = mockCurrentMonsters;

            Assert.IsTrue( systemUnderTest.DoesMoveMatchAnyCurrentMonsters( mockMove ) );
        }

        [Test]
        public void DoesMoveMatchAnyCurrentMonsters_ReturnsFalse_WhenNoMonsterMatches() {
            List<IGamePiece> mockMove = new List<IGamePiece>();
            List<IGameMonster> mockCurrentMonsters = new List<IGameMonster>();
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( false ) );
            systemUnderTest.CurrentMonsters = mockCurrentMonsters;

            Assert.IsFalse( systemUnderTest.DoesMoveMatchAnyCurrentMonsters( mockMove ) );
        }

        [Test]
        public void WhenProcessingPlayerMove_AnyMatchingCurrentMonsters_GetAttacked() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<IGamePiece> mockMove = new List<IGamePiece>();

            List<IGameMonster> mockCurrentMonsters = new List<IGameMonster>();
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( true ) );
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( false ) );
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( false ) );

            List<IGameMonster> mockRemainingMonsters = new List<IGameMonster>();
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( true ) );
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( false ) );
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( false ) );

            systemUnderTest.CurrentMonsters = mockCurrentMonsters;
            systemUnderTest.RemainingMonsters = mockRemainingMonsters;

            systemUnderTest.ProcessPlayerMoveOnCurrentMonsters( mockPlayer, mockMove );

            mockCurrentMonsters[0].Received( 1 ).AttackedByPlayer( mockPlayer );
            mockCurrentMonsters[1].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockCurrentMonsters[2].Received( 0 ).AttackedByPlayer( mockPlayer );

            mockRemainingMonsters[0].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockRemainingMonsters[1].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockRemainingMonsters[2].Received( 0 ).AttackedByPlayer( mockPlayer );
        }

        [Test]
        public void DeadMonstersGetRemovedFromCurrentList() {
            IGameMonster mock1 = GetMockMonsterWithIsDead( true );
            IGameMonster mock2 = GetMockMonsterWithIsDead( false );
            IGameMonster mock3 = GetMockMonsterWithIsDead( false );
            IGameMonster mock4 = GetMockMonsterWithIsDead( true );
            List<IGameMonster> mockCurrentMonsters = new List<IGameMonster>() { mock1, mock2, mock3, mock4 };

            systemUnderTest.CurrentMonsters = mockCurrentMonsters;

            systemUnderTest.RemoveDeadMonstersFromCurrentList();

            Assert.AreEqual( 2, systemUnderTest.CurrentMonsters.Count );
            Assert.Contains( mock2, systemUnderTest.CurrentMonsters );
            Assert.Contains( mock3, systemUnderTest.CurrentMonsters );
        }

        [Test]
        public void WhenSufficientMonstersInRemainingList_TransferredToCurrentListAsExpected() {
            GameRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.FillCurrentMonstersWithRemaining();

            Assert.AreEqual( 4, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void WhenTooFewMonstersInRemainingList_TransferredAllToCurrentList() {
            GameRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.FillCurrentMonstersWithRemaining();

            Assert.AreEqual( 3, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void WhenNoMonstersInRemainingList_NoneTransferredToCurrentList() {
            GameRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();

            systemUnderTest.FillCurrentMonstersWithRemaining();

            Assert.AreEqual( 2, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void AfterProcessingMove_IfNoMonstersInCurrentList_MessageIsSent() {
            GameRules.GetActiveMonsterCount().Returns( 4 );
            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.RemainingMonsters = new List<IGameMonster>();

            systemUnderTest.SendMessageIfAllMonstersDead();

            GameManager.Received().OnAllMonstersDead();
        }

        [Test]
        public void WhenCreatingMonsterManagerWithAllMonsters_CurrentAndRemainingListsAsExpected() {
            GameRules.GetActiveMonsterCount().Returns( 4 );
            List<IGameMonster> allMonsters = new List<IGameMonster>();
            allMonsters.Add( Substitute.For<IGameMonster>() );
            allMonsters.Add( Substitute.For<IGameMonster>() );
            allMonsters.Add( Substitute.For<IGameMonster>() );
            allMonsters.Add( Substitute.For<IGameMonster>() );
            allMonsters.Add( Substitute.For<IGameMonster>() );
            allMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.SetMonsters( allMonsters );

            Assert.AreEqual( 4, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 2, systemUnderTest.RemainingMonsters.Count );
        }

        private IGameMonster GetMockMonsterWithMatchCombo( bool i_doesMatch ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.DoesMatchCombo( Arg.Any<List<IGamePiece>>() ).Returns( i_doesMatch );

            return mockMonster;
        }

        private IGameMonster GetMockMonsterWithIsDead( bool i_isDead ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.IsDead().Returns( i_isDead );

            return mockMonster;
        }
    }
}