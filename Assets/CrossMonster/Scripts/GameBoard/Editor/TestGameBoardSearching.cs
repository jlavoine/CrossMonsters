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
    public class TestGameBoardSearching : ZenjectUnitTestFixture {
        [Inject]
        IGameRules GameRules;

        [Inject]
        GameBoard systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.Bind<IMonsterManager>().FromInstance( Substitute.For<IMonsterManager>() );
            Container.BindFactory<int, GamePiece, GamePiece.Factory>();
            Container.Bind<GameBoard>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void TestSearch_1() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            systemUnderTest.InitBoardForTesting( mockBoard );

            bool isOnBoard = systemUnderTest.IsComboOnBoard( new List<int>() { 0, 0, 0 } );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_2() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            systemUnderTest.InitBoardForTesting( mockBoard );

            bool isOnBoard = systemUnderTest.IsComboOnBoard( new List<int>() { 1, 2, 3 } );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_3() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(1), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            systemUnderTest.InitBoardForTesting( mockBoard );

            bool isOnBoard = systemUnderTest.IsComboOnBoard( new List<int>() { 0, 0, 2, 1 } );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_4() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 1 ), new GamePiece( 1 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 2 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 1 ) }};

            systemUnderTest.InitBoardForTesting( mockBoard );

            bool isOnBoard = systemUnderTest.IsComboOnBoard( new List<int>() { 3, 0, 1, 2, 1 } );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_5() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            systemUnderTest.InitBoardForTesting( mockBoard );

            bool isOnBoard = systemUnderTest.IsComboOnBoard( new List<int>() { 3, 3, 3 } );
            Assert.IsFalse( isOnBoard );
        }
    }
}
