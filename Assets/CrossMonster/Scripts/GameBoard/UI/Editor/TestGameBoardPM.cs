using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameBoardPM : CrossMonstersUnitTest {

        // can't test this right now due to bug in NSubstitute subbing multidimensional arrays.
        // this seems fixed in 1.8.1, but I don't think Unity Test Tools is on that version yet...
        /*[Test]
        public void AfterCreating_GamePiecePMs_EqualBoardSize() {
            IGameBoard mockBoard = CreateGameBoardMock( 4 );
            GameBoardPM systemUnderTest = new GameBoardPM( mockBoard );

            Assert.AreEqual( 16, systemUnderTest.GamePiecePMs.Count );
        }

        private IGameBoard CreateGameBoardMock( int i_size ) {
            IGamePiece[,] mockPieces = new IGamePiece[i_size, i_size];
            IGameBoard mockBoard = Substitute.For<IGameBoard>();

            for ( int i = 0; i < mockPieces.GetLength(0); ++i ) {
                for ( int j = 0; j < mockPieces.GetLength( 1 ); ++j ) {
                    mockPieces[i, j] = Substitute.For<IGamePiece>();
                }
            }
            mockBoard.BoardPieces.Returns( mockPieces );

            return mockBoard;
        }*/
    }
}