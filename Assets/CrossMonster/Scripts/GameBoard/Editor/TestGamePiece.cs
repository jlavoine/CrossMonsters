using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePiece : CrossMonstersUnitTest {

        [Test]
        public void WhenCreatingPiece_PieceTypeMatchesConstructor() {
            GamePiece systemUnderTest = new GamePiece( 5 );

            Assert.AreEqual( 5, systemUnderTest.PieceType );
        }

        [Test]
        public void WhenUsingPiece_PieceTypeRotatesAccordingToGameRules() {
            GameRules.Instance.GetGamePieceRotation( 0 ).Returns( 3 );
            GamePiece systemUnderTest = new GamePiece( 0 );

            systemUnderTest.UsePiece();

            Assert.AreEqual( 3, systemUnderTest.PieceType );
        }
    }
}
