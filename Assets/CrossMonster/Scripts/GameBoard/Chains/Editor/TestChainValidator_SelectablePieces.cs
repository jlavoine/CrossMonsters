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
    public class TestChainValidator_SelectablePieces : ZenjectUnitTestFixture {

        [Inject]
        ChainValidator_SelectablePieces systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ChainValidator_SelectablePieces>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IsValidPiece_IsTrue_IfIncomingPiece_IsSelectable() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.IsSelectable().Returns( true );
            bool isValid = systemUnderTest.IsValidPieceInChain( mockPiece, new List<IGamePiece>() );

            Assert.IsTrue( isValid );
        }

        [Test]
        public void IsValidPiece_IsFalse_IfIncomingPiece_IsNotSelectable() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.IsSelectable().Returns( false );
            bool isValid = systemUnderTest.IsValidPieceInChain( mockPiece, new List<IGamePiece>() );

            Assert.IsFalse( isValid );
        }
    }
}