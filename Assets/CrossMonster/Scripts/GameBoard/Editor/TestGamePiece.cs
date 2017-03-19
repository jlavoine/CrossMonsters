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
            Container.BindFactory<int, GamePiece, GamePiece.Factory>();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreatingPiece_PieceTypeMatchesConstructor() {
            GamePiece systemUnderTest = SystemFactory.Create( 5 );

            Assert.AreEqual( 5, systemUnderTest.PieceType );
        }

        [Test]
        public void WhenUsingPiece_IfShouldNotRotate_PieceDoesNotRotate() {
            DungeonManager.Data.ShouldRotatePieces().Returns( false );
            GamePiece systemUnderTest = SystemFactory.Create( 5 );

            systemUnderTest.UsePiece();

            Assert.AreEqual( 5, systemUnderTest.PieceType );
        }
    }
}
