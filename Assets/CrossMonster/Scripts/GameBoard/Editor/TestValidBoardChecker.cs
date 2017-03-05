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
    public class TestValidBoardChecker : ZenjectUnitTestFixture {

        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        ValidBoardChecker systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterManager>().FromInstance( Substitute.For<IMonsterManager>() );
            Container.Bind<ValidBoardChecker>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void TestSearch_1() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            MonsterManager.GetCurrentMonsterCombos().Returns( new List<List<int>>() { new List<int>() { 0, 0, 0 } } );

            bool isOnBoard = systemUnderTest.IsMonsterComboAvailableOnBoard( mockBoard );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_2() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            MonsterManager.GetCurrentMonsterCombos().Returns( new List<List<int>>() { new List<int>() { 1, 2, 3 } } );

            bool isOnBoard = systemUnderTest.IsMonsterComboAvailableOnBoard( mockBoard );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_3() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(1), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            MonsterManager.GetCurrentMonsterCombos().Returns( new List<List<int>>() { new List<int>() { 0, 0, 2, 1 } } );

            bool isOnBoard = systemUnderTest.IsMonsterComboAvailableOnBoard( mockBoard );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_4() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 1 ), new GamePiece( 1 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 2 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 1 ) }};

            MonsterManager.GetCurrentMonsterCombos().Returns( new List<List<int>>() { new List<int>() { 3, 0, 1, 2, 1 } } );

            bool isOnBoard = systemUnderTest.IsMonsterComboAvailableOnBoard( mockBoard );
            Assert.IsTrue( isOnBoard );
        }

        [Test]
        public void TestSearch_5() {
            IGamePiece[,] mockBoard = new IGamePiece[,] { { new GamePiece(0), new GamePiece( 0 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 1 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 2 ), new GamePiece( 0 ), new GamePiece( 0 ) },
            { new GamePiece(0), new GamePiece( 3 ), new GamePiece( 0 ), new GamePiece( 0 ) }};

            MonsterManager.GetCurrentMonsterCombos().Returns( new List<List<int>>() { new List<int>() { 3, 3, 3 } } );

            bool isOnBoard = systemUnderTest.IsMonsterComboAvailableOnBoard( mockBoard );
            Assert.IsFalse( isOnBoard );
        }
    }
}
