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
        GamePiece.Factory SystemFactory;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IGameRules>().FromInstance( Substitute.For<IGameRules>() );
            Container.BindFactory<int, GamePiece, GamePiece.Factory>();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreatingPiece_PieceTypeMatchesConstructor() {
            GamePiece systemUnderTest = SystemFactory.Create( 5 );

            Assert.AreEqual( 5, systemUnderTest.PieceType );
        }
    }
}
