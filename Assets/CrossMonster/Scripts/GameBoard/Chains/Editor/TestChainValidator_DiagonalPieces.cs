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

namespace CrossMonsters {
    [TestFixture]
    public class TestChainValidator_DiagonalPieces : ZenjectUnitTestFixture {
        const int TEST_BOARD_SIZE = 6;

        [Inject]
        ChainValidator_DiagonalPieces systemUnderTest;

        [Inject]
        IGameRules MockGameRules;

        [Inject]
        ICurrentDungeonGameManager MockCurrentDungeon;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.Bind<ICurrentDungeonGameManager>().FromInstance( Substitute.For<ICurrentDungeonGameManager>() );
            Container.Bind<ChainValidator_DiagonalPieces>().AsSingle();
            Container.Inject( this );

            MockGameRules.GetBoardSize().Returns( TEST_BOARD_SIZE );
        }

        [Test]
        public void IfIncomingChainIsEmpty_Validates() {
            SetCurrentDungeonToAllowDiagonals( false );
            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() );
        }

        static object[] LeftEdgeTests = {
            new object[] { 7, 0, false },
            new object[] { 1, 6, false },
            new object[] { 13, 6, false },
            new object[] { 6, 0, true },
            new object[] { 7, 12, false },
            new object[] { 12, 6, true },
            new object[] { 1, 0, true }
        };

        [Test, TestCaseSource( "LeftEdgeTests" )]
        public void TestLeftEdgeDiagonalValidation( int i_incomingPieceIndex, int i_lastPlayedPieceIndex, bool i_expectedValidation ) {
            SetCurrentDungeonToAllowDiagonals( false );
            RunDiagonalTest( i_incomingPieceIndex, i_lastPlayedPieceIndex, i_expectedValidation );
        }

        static object[] RightEdgeTests = {
            new object[] { 10, 5, false },
            new object[] { 4, 11, false },
            new object[] { 16, 11, false },
            new object[] { 11, 5, true },
            new object[] { 10, 17, false },
            new object[] { 17, 11, true },
            new object[] { 4, 5, true }
        };

        [Test, TestCaseSource( "RightEdgeTests" )]
        public void TestRightEdgeDiagonalValidation( int i_incomingPieceIndex, int i_lastPlayedPieceIndex, bool i_expectedValidation ) {
            SetCurrentDungeonToAllowDiagonals( false );
            RunDiagonalTest( i_incomingPieceIndex, i_lastPlayedPieceIndex, i_expectedValidation );
        }

        static object[] MiddlePieceTests = {
            new object[] { 0, 7, false },
            new object[] { 2, 7, false },
            new object[] { 12, 7, false },
            new object[] { 14, 7, false },
            new object[] { 3, 8, false },
            new object[] { 1, 8, false },
            new object[] { 13, 8, false },
            new object[] { 15, 8, false },
            new object[] { 2, 8, true },
            new object[] { 7, 8, true },
            new object[] { 9, 8, true },
            new object[] { 14, 8, true },
            new object[] { 17, 10, false },
            new object[] { 5, 10, false }
        };

        [Test, TestCaseSource( "MiddlePieceTests" )]
        public void TestMiddlePieceDiagonalValidation( int i_incomingPieceIndex, int i_lastPlayedPieceIndex, bool i_expectedValidation ) {
            SetCurrentDungeonToAllowDiagonals( false );
            RunDiagonalTest( i_incomingPieceIndex, i_lastPlayedPieceIndex, i_expectedValidation );
        }

        /// <summary>
        /// Could run this test with all sources, but I think just one is fine.
        /// </summary>        
        [Test, TestCaseSource( "MiddlePieceTests" )]
        public void WhenDungeonAllowsDiagonals_AllMovesValid( int i_incomingPieceIndex, int i_lastPlayedPieceIndex, bool i_expectedValidation ) {
            SetCurrentDungeonToAllowDiagonals( true );
            RunDiagonalTest( i_incomingPieceIndex, i_lastPlayedPieceIndex, true );
        }

        private void RunDiagonalTest( int i_incomingPieceIndex, int i_lastPlayedPieceIndex, bool i_expectedValidation ) {
            IGamePiece mockIncomingPiece = CreateMockPieceWithIndex( i_incomingPieceIndex );
            IGamePiece mockLastPlayedPiece = CreateMockPieceWithIndex( i_lastPlayedPieceIndex );

            List<IGamePiece> mockChain = new List<IGamePiece>();
            mockChain.Add( mockLastPlayedPiece );

            bool isValid = systemUnderTest.IsValidPieceInChain( mockIncomingPiece, mockChain );

            Assert.AreEqual( i_expectedValidation, isValid );
        }

        private IGamePiece CreateMockPieceWithIndex( int i_index ) {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.Index.Returns( i_index );

            return mockPiece;
        }

        private void SetCurrentDungeonToAllowDiagonals( bool i_allow ) {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.AllowDiagonals().Returns( i_allow );

            MockCurrentDungeon.Data.Returns( mockData );
        }
    }
}
