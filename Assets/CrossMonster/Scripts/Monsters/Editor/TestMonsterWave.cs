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
    public class TestMonsterWave : ZenjectUnitTestFixture {
        IGameRules MockRules;
        MonsterWave systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            MockRules = Substitute.For<IGameRules>();
            systemUnderTest = new MonsterWave( MockRules, Substitute.For<IMonsterWaveData>() );
        }

        [Test]
        public void WhenTicks_CurrentMonstersTick() {
            List<IGameMonster> mockMonsters = new List<IGameMonster>();
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.CurrentMonsters = mockMonsters;
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

            systemUnderTest.ProcessPlayerMove( mockPlayer, mockMove );

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
            MockRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.FillCurrentMonstersFromRemainingMonsters();

            Assert.AreEqual( 4, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void WhenTooFewMonstersInRemainingList_TransferredAllToCurrentList() {
            MockRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();
            systemUnderTest.RemainingMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.FillCurrentMonstersFromRemainingMonsters();

            Assert.AreEqual( 3, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void WhenNoMonstersInRemainingList_NoneTransferredToCurrentList() {
            MockRules.GetActiveMonsterCount().Returns( 4 );

            systemUnderTest.CurrentMonsters = new List<IGameMonster>();
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );
            systemUnderTest.CurrentMonsters.Add( Substitute.For<IGameMonster>() );

            systemUnderTest.RemainingMonsters = new List<IGameMonster>();

            systemUnderTest.FillCurrentMonstersFromRemainingMonsters();

            Assert.AreEqual( 2, systemUnderTest.CurrentMonsters.Count );
            Assert.AreEqual( 0, systemUnderTest.RemainingMonsters.Count );
        }

        [Test]
        public void IsCleared_ReturnsTrue_IfNoCurrentMonsters() {
            systemUnderTest.CurrentMonsters = new List<IGameMonster>() { };

            Assert.IsTrue( systemUnderTest.IsCleared() );
        }

        [Test]
        public void IsCleared_ReturnsFalse_IfCurrentMonsters() {
            systemUnderTest.CurrentMonsters = new List<IGameMonster>() { Substitute.For<IGameMonster>() };

            Assert.IsFalse( systemUnderTest.IsCleared() );
        }

        [Test]
        public void GetMaxLength_ReturnsLengthOfLongestCombo() {
            IGameMonster monster1 = GetMockMonsterWithComboLength( 7 );
            IGameMonster monster2 = GetMockMonsterWithComboLength( 1 );
            IGameMonster monster3 = GetMockMonsterWithComboLength( 3 );
            systemUnderTest.CurrentMonsters = new List<IGameMonster>() { monster1, monster2, monster3 };

            Assert.AreEqual( 7, systemUnderTest.GetLongestCombo() );
        }

        private IGameMonster GetMockMonsterWithComboLength( int i_length ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.AttackCombo = new List<int>();
            
            for ( int i = 0; i < i_length; ++i ) {
                mockMonster.AttackCombo.Add( i );
            }

            return mockMonster;
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