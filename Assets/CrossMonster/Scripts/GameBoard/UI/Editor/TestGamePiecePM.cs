using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestGamePiecePM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePiece>() );

            MyMessenger.Instance.Received().AddListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
            MyMessenger.Instance.Received().AddListener( GameMessages.CHAIN_RESET, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePiece>() );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
            MyMessenger.Instance.Received().RemoveListener( GameMessages.CHAIN_RESET, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreating_PieceTypeProperty_MatchesPieceType() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.PieceType.Returns( 3 );

            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_IsOnProperty_IsFalse() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePiece>() );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfThisPMsPiece_IsOnProperty_IsTrue() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            systemUnderTest.OnPieceAddedToChain( mockPiece );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfNotPMsPiece_IsOnProperty_IsFalse() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            IGamePiece notSamePiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            systemUnderTest.OnPieceAddedToChain( notSamePiece );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenChainIsReset_IsOnProperty_IsFalse() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePiece>() );
            systemUnderTest.ViewModel.SetProperty( GamePiecePM.IS_ON_PROPERTY, Color.yellow );

            systemUnderTest.OnChainReset();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenModelUpdated_PropertiesAsExpected() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            mockPiece.PieceType.Returns( 0 );
            GamePiecePM systemUnderTest = new GamePiecePM( mockPiece );

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );

            mockPiece.PieceType.Returns( 1 );
            mockPiece.ModelUpdated += Raise.Event<ModelUpdateHandler>();
            Assert.AreEqual( "1", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
        }
    }
}
