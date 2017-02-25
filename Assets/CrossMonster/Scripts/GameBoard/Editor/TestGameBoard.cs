using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameBoard : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_GameBoardSizeIsExpected() {
            GameRules.Instance.GetBoardSize().Returns( 4 );
            GameRules.Instance.GetPieceTypes().Returns( new List<int>() { 1, 2, 3 } );

            GameBoard systemUnderTest = new GameBoard();

            Assert.AreEqual( 16, systemUnderTest.Board.Length );
        }

        [Test]
        public void WhenCreating_AllGamePieces_HaveTypeFromRules() {
            List<int> pieceTypes = new List<int>() { 1, 2, 3, 4 };
            GameRules.Instance.GetBoardSize().Returns( 6 );
            GameRules.Instance.GetPieceTypes().Returns( pieceTypes );

            GameBoard systemUnderTest = new GameBoard();

            foreach ( IGamePiece piece in systemUnderTest.Board ) {
                Assert.Contains( piece.PieceType, pieceTypes );
            }
        }
    }
}
