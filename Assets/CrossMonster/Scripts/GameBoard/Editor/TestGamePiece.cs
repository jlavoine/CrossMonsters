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
    public class TestGamePiece : ZenjectUnitTestFixture {
        [Inject]
        IGameRules GameRules;

        [Inject]
        ICurrentDungeonGameManager DungeonManager;

        [Inject]
        GamePiece.Factory SystemFactory;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.Bind<ICurrentDungeonGameManager>().FromInstance( Substitute.For<ICurrentDungeonGameManager>() );
            Container.BindFactory<int, int, GamePiece, GamePiece.Factory>();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreatingPiece_PropertiesMatchConstructor() {
            GamePiece systemUnderTest = SystemFactory.Create( 5, 11 );

            Assert.AreEqual( 5, systemUnderTest.PieceType );
            Assert.AreEqual( 11, systemUnderTest.Index );
        }

        [Test]
        public void WhenUsingPiece_IfShouldNotRotate_PieceDoesNotRotate() {
            DungeonManager.Data.ShouldRotatePieces().Returns( false );
            GamePiece systemUnderTest = SystemFactory.Create( 5, 0 );

            systemUnderTest.UsePiece();

            Assert.AreEqual( 5, systemUnderTest.PieceType );
        }
    }
}
