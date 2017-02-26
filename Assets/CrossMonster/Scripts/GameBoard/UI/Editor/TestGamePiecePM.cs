﻿using NUnit.Framework;
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

        [Test]
        public void WhenCreating_BackgroundColorProperty_IsDefault() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePiece>() );

            Assert.AreEqual( Color.white, systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.BG_COLOR_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfThisPMsPiece_BackgroundColorPropertyAsExpected() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            systemUnderTest.OnPieceAddedToChain( mockPiece );

            Assert.AreEqual( Color.yellow, systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.BG_COLOR_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfNotPMsPiece_BackgroundColorIsDefault() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            IGamePiece notSamePiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            systemUnderTest.OnPieceAddedToChain( notSamePiece );

            Assert.AreEqual( Color.white, systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.BG_COLOR_PROPERTY ) );
        }
    }
}
