using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePiecePM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_PieceTypeProperty_MatchesPieceType() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.PieceType.Returns( 3 );

            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
        }
    }
}
