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
    public class TestChainValidator_DuplicatePieces : ZenjectUnitTestFixture {

        [Test]
        public void IsValidPiece_IsTrue_IfIncomingPiece_IsNotAlreadyInChain() {
            ChainValidator_DuplicatePieces systemUnderTest = new ChainValidator_DuplicatePieces();

            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() );

            Assert.IsTrue( isValid );
        }

        [Test]
        public void IsValidPiece_IsFalse_IfIncomingPiee_IAlreadyInChain() {
            ChainValidator_DuplicatePieces systemUnderTest = new ChainValidator_DuplicatePieces();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            List<IGamePiece> mockChain = new List<IGamePiece>() { mockPiece };

            bool isValid = systemUnderTest.IsValidPieceInChain( mockPiece, mockChain );

            Assert.IsFalse( isValid );
        }
    }
}