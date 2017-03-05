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
    public class TestGameBoard : ZenjectUnitTestFixture {
        [Inject]
        IGameRules GameRules;

        [Inject]
        GameBoard systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.BindFactory<int, GamePiece, GamePiece.Factory>();
            Container.Bind<GameBoard>().AsSingle();
            Container.Inject( this );            
        }

        [Test]
        public void WhenCreating_GameBoardSizeIsExpected() {
            InitBoardWithSizeAndList( 4, new List<int>() { 1, 2, 3 } );

            Assert.AreEqual( 16, systemUnderTest.BoardPieces.Length );
        }

        [Test]
        public void WhenCreating_AllGamePieces_HaveTypeFromRules() {
            List<int> pieceTypes = new List<int>() { 1, 2, 3, 4 };
            InitBoardWithSizeAndList( 6, pieceTypes );

            foreach ( IGamePiece piece in systemUnderTest.BoardPieces ) {
                Assert.Contains( piece.PieceType, pieceTypes );
            }
        }

        [Test, Ignore("This doesn't work because the game pieces created are actual implementations")]
        public void WhenRandomizingBoard_AllPiecesAreRandomized() {
            InitBoardWithSizeAndList( 4, new List<int>() { 1, 2, 3 } );

            systemUnderTest.RandomizeBoard();

            foreach ( IGamePiece piece in systemUnderTest.BoardPieces ) {
                piece.Received( 1 ).Randomize();
            }
        }

        private void InitBoardWithSizeAndList( int i_size, List<int> i_pieceTypes ) {
            GameRules.GetBoardSize().Returns( i_size );
            GameRules.GetPieceTypes().Returns( i_pieceTypes );
            systemUnderTest.Initialize();
        }
    }
}
