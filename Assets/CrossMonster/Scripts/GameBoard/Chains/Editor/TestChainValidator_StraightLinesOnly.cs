using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

/// <summary>
/// NOTE: All tests performed on a 6x3 board.
/// </summary>

namespace MonsterMatch {
    [TestFixture]
    public class TestChainValidator_StraightLinesOnly : ZenjectUnitTestFixture {
        [Inject]
        ChainValidator_StraightLinesOnly systemUnderTest;

        [Inject]
        ICurrentDungeonGameManager MockCurrentDungeon;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ICurrentDungeonGameManager>().FromInstance( Substitute.For<ICurrentDungeonGameManager>() );
            Container.Bind<ChainValidator_StraightLinesOnly>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IfIncomingChainIsEmpty_Validates() {
            SetCurrentDungeonToAllowStraightLinesOnly( true );
            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() );

            Assert.IsTrue( isValid );
        }

        [Test]
        public void IfIncomingChainIsOnlyOnePiece_Validates() {
            SetCurrentDungeonToAllowStraightLinesOnly( true );
            List<IGamePiece> mockChain = new List<IGamePiece>() { Substitute.For<IGamePiece>() };

            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), mockChain );

            Assert.IsTrue( isValid );
        }

        static object[] StraightLineOnlyTests = {
            new object[] { 0, 1, 2, true },
            new object[] { 0, 1, 7, false },
            new object[] { 0, 1, 8, false },
            new object[] { 0, 6, 12, true },
            new object[] { 0, 6, 7, false },
            new object[] { 9, 8, 7, true },
            new object[] { 9, 8, 14, false },
            new object[] { 0, 7, 14, true },
            new object[] { 0, 7, 13, false },
            new object[] { 12, 7, 2, true },
            new object[] { 12, 7, 8, false },
            new object[] { 17, 11, 5, true }
        };

        [Test, TestCaseSource( "StraightLineOnlyTests" )]
        public void TestStraightLineOnlyLogic( int i_firstIndex, int i_secondIndex, int i_incomingIndex, bool i_expectedValidation ) {
            SetCurrentDungeonToAllowStraightLinesOnly( true );
            List<IGamePiece> mockChain = CreateChainFromTwoIndices( i_firstIndex, i_secondIndex );
            IGamePiece mockIncomingPiece = CreateMockPieceWithIndex( i_incomingIndex );

            bool isValid = systemUnderTest.IsValidPieceInChain( mockIncomingPiece, mockChain );

            Assert.AreEqual( i_expectedValidation, isValid );
        }

        private List<IGamePiece> CreateChainFromTwoIndices( int i_first, int i_second ) {
            List<IGamePiece> mockChain = new List<IGamePiece>();
            IGamePiece firstPiece = CreateMockPieceWithIndex( i_first );
            IGamePiece secondPiece = CreateMockPieceWithIndex( i_second );

            mockChain.Add( firstPiece );
            mockChain.Add( secondPiece );

            return mockChain;
        }

        private void SetCurrentDungeonToAllowStraightLinesOnly( bool i_allow ) {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.StraightMovesOnly().Returns( i_allow );

            MockCurrentDungeon.Data.Returns( mockData );
        }

        private IGamePiece CreateMockPieceWithIndex( int i_index ) {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.Index.Returns( i_index );

            return mockPiece;
        }
    }
}
