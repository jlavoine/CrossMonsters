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
        private IAudioManager MockAudio;
        private IGamePiece MockPiece;

        [SetUp]
        public override void BeforeTest() {
            base.BeforeTest();
            MockAudio = Substitute.For<IAudioManager>();
            MockPiece = Substitute.For<IGamePiece>();
        }

        private GamePiecePM CreateSystem() {
            GamePiecePM systemUnderTest = new GamePiecePM( MockAudio, MockPiece );
            return systemUnderTest;
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = CreateSystem();

            MyMessenger.Instance.Received().AddListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
            MyMessenger.Instance.Received().AddListener( GameMessages.CHAIN_RESET, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
            MyMessenger.Instance.Received().RemoveListener( GameMessages.CHAIN_RESET, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreating_PieceTypeProperty_MatchesPieceType() {
            MockPiece.PieceType.Returns( 3 );

            GamePiecePM systemUnderTest = CreateSystem();

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_IsOnProperty_IsFalse() {
            GamePiecePM systemUnderTest = CreateSystem();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfThisPMsPiece_IsOnProperty_IsTrue() {
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.OnPieceAddedToChain( MockPiece );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfThisPMsPiece_SoundIsPlayed() {
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.OnPieceAddedToChain( MockPiece );

            MockAudio.Received().PlayOneShot( CombatAudioKeys.ADD_TO_CHAIN );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfNotPMsPiece_IsOnProperty_IsFalse() {
            IGamePiece notSamePiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.OnPieceAddedToChain( notSamePiece );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceIsAddedToChain_IfNotPMsPiece_NoSoundIsPlayed() {
            IGamePiece notSamePiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.OnPieceAddedToChain( notSamePiece );

            MockAudio.DidNotReceive().PlayOneShot( CombatAudioKeys.ADD_TO_CHAIN );
        }

        [Test]
        public void WhenChainIsReset_IsOnProperty_IsFalse() {
            GamePiecePM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( GamePiecePM.IS_ON_PROPERTY, Color.yellow );

            systemUnderTest.OnChainReset();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenModelUpdated_PropertiesAsExpected() {
            MockPiece.PieceType.Returns( 0 );
            GamePiecePM systemUnderTest = CreateSystem();

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );

            MockPiece.PieceType.Returns( 1 );
            MockPiece.ModelUpdated += Raise.Event<ModelUpdateHandler>();
            Assert.AreEqual( "1", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
        }
    }
}
