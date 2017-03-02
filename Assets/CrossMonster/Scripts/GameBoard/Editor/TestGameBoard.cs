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
            GameRules.GetBoardSize().Returns( 4 );
            GameRules.GetPieceTypes().Returns( new List<int>() { 1, 2, 3 } );
            systemUnderTest.Initialize();

            Assert.AreEqual( 16, systemUnderTest.BoardPieces.Length );
        }

        [Test]
        public void WhenCreating_AllGamePieces_HaveTypeFromRules() {
            List<int> pieceTypes = new List<int>() { 1, 2, 3, 4 };
            GameRules.GetBoardSize().Returns( 6 );
            GameRules.GetPieceTypes().Returns( pieceTypes );
            systemUnderTest.Initialize();

            foreach ( IGamePiece piece in systemUnderTest.BoardPieces ) {
                Assert.Contains( piece.PieceType, pieceTypes );
            }
        }
    }
}
