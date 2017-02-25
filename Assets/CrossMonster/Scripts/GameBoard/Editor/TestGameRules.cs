using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameRules : CrossMonstersUnitTest {

        [Test]
        public void WhenGameRulesRotationDictionaryContainsPieceType_ReturnsMatchValue() {
            Dictionary<int, int> mockPieceRotations = new Dictionary<int, int>();
            mockPieceRotations.Add( 3, 6 );

            GameRules systemUnderTest = new GameRules();
            systemUnderTest.PieceRotations = mockPieceRotations;

            int pieceToRotateTo = systemUnderTest.GetGamePieceRotation( 3 );
            Assert.AreEqual( 6, pieceToRotateTo );
        }

        [Test]
        public void WhenGameRulesRotationDictionaryDoesNotContainPieceType_InputValueIsReturned() {
            Dictionary<int, int> mockPieceRotations = new Dictionary<int, int>();
            mockPieceRotations.Add( 3, 6 );

            GameRules systemUnderTest = new GameRules();
            systemUnderTest.PieceRotations = mockPieceRotations;

            int pieceToRotateTo = systemUnderTest.GetGamePieceRotation( 0 );
            Assert.AreEqual( 0, pieceToRotateTo );
        }
    }
}
