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
        private IGamePiece MockPiece;

        [SetUp]
        public override void BeforeTest() {
            base.BeforeTest();
            MockPiece = Substitute.For<IGamePiece>();
        }

        private GamePiecePM CreateSystem() {
            GamePiecePM systemUnderTest = new GamePiecePM( MockPiece );
            return systemUnderTest;
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = CreateSystem();

            MyMessenger.Instance.Received().AddListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, Arg.Any<Callback<IGamePiece>>() );
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
        public void WhenPieceIsAddedToChain_IfNotPMsPiece_IsOnProperty_IsFalse() {
            IGamePiece notSamePiece = Substitute.For<IGamePiece>();
            GamePiecePM systemUnderTest = CreateSystem();

            systemUnderTest.OnPieceAddedToChain( notSamePiece );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceAnimation_IsComplete_IsOnProperty_IsFalse() {
            GamePiecePM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( GamePiecePM.IS_ON_PROPERTY, Color.yellow );

            systemUnderTest.OnAnimationComplete();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( GamePiecePM.IS_ON_PROPERTY ) );
        }

        [Test]
        public void WhenPieceAnimation_IsComplete_PieceState_IsSelectable() {
            GamePiecePM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( GamePiecePM.IS_ON_PROPERTY, Color.yellow );

            systemUnderTest.OnAnimationComplete();

            MockPiece.Received().State = GamePieceStates.Selectable;
        }

        [Test]
        public void WhenModelUpdated_PropertiesAsExpected() {
            MockPiece.PieceType.Returns( 0 );
            MockPiece.State = GamePieceStates.Correct;
            GamePiecePM systemUnderTest = CreateSystem();

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
            Assert.AreEqual( GamePiecePM.CHAIN_COMPLETE_TRIGGER, systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.TRIGGER_PROPERTY + "0" ) );

            MockPiece.PieceType.Returns( 1 );
            MockPiece.State.Returns( GamePieceStates.Incorrect );
            MockPiece.ModelUpdated += Raise.Event<ModelUpdateHandler>();

            Assert.AreEqual( "1", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.PIECE_TYPE_PROPERTY ) );
            Assert.AreEqual( GamePiecePM.CHAIN_DROPPED_TRIGGER, systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.TRIGGER_PROPERTY + "1" ) );
        }

        static object[] AnimStateTests = {
            new object[] { GamePieceStates.Selectable, string.Empty },
            new object[] { GamePieceStates.Correct, GamePiecePM.CHAIN_COMPLETE_TRIGGER },
            new object[] { GamePieceStates.Incorrect, GamePiecePM.CHAIN_DROPPED_TRIGGER },
        };

        [Test, TestCaseSource( "AnimStateTests" )]
        public void GetAnimState_ReturnsExpected_BasedOnState( GamePieceStates i_state, string i_expectedValue ) {
            GamePiecePM systemUnderTest = CreateSystem();

            string state = systemUnderTest.GetAnimState( i_state );

            Assert.AreEqual( i_expectedValue, state );
        }
    }
}
