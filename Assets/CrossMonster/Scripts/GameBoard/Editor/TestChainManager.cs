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
    public class TestChainManager : ZenjectUnitTestFixture {

        [Inject]
        ChainManager systemUnderTest;

        [Inject]
        IChainProcessor ChainProcessor;

        [Inject]
        IMessageService MyMessenger;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainProcessor>().FromInstance( Substitute.For<IChainProcessor>() );
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<ChainManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            //ChainManager systemUnderTest = new ChainManager();
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            systemUnderTest.Dispose();
        }

        static object[] IsNoChainTests = {
            new object[] { null, true },
            new object[] { new List<IGamePiece>(), false },
            new object[] { new List<IGamePiece>() { Substitute.For<IGamePiece>() }, false }
        };

        [Test, TestCaseSource("IsNoChainTests")]
        public void IsNoChain_ReturnsAsExpected( List<IGamePiece> i_chain, bool i_expected ) {
            systemUnderTest.Chain = i_chain;

            Assert.AreEqual( i_expected, systemUnderTest.IsNoChain() );
        }

        [Test]
        public void WhenNoChain_StartChain_CreatesChain() {
            CreateChainManager_WithNoChain();

            systemUnderTest.StartChain( Substitute.For<IGamePiece>() );

            Assert.IsNotNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenAlreadyChain_StartChain_DoesNotChangeChain() {
            List<IGamePiece> mockChain = new List<IGamePiece>();
            systemUnderTest.Chain = mockChain;

            systemUnderTest.StartChain( Substitute.For<IGamePiece>() );

            Assert.AreEqual( mockChain, systemUnderTest.Chain );
        }

        [Test]
        public void WhenNoChain_StartChain_AddsPieceToChain() {
            CreateChainManager_WithNoChain();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();

            systemUnderTest.StartChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        [Test]
        public void WhenNoChain_StartChain_SendsMessageWithAddedPiece() {
            CreateChainManager_WithNoChain();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();

            systemUnderTest.StartChain( mockPiece );

            MyMessenger.Received().Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, mockPiece );
        }

        [Test]
        public void WhenNoChain_ContinuingChain_DoesNothing() {
            CreateChainManager_WithNoChain();

            systemUnderTest.ContinueChain( Substitute.For<IGamePiece>() );

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_AddPieceToChain() {
            CreateChainManager_WithEmptyActiveChain();

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_SendsPieceAddedEvent() {
            CreateChainManager_WithEmptyActiveChain();

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            MyMessenger.Received().Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, mockPiece );
        }

        [Test]
        public void WhenContinuingChain_DuplicatePiecesNotAdded() {
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.Chain = new List<IGamePiece>() { mockPiece };

            systemUnderTest.ContinueChain( mockPiece );

            Assert.AreEqual( 1, systemUnderTest.Chain.Count );
        }

        [Test]
        public void WhenChainCanceled_ChainIsNull() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.CancelChain();

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenChainCanceled_ChainResetEventIsSent() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.CancelChain();

            MyMessenger.Received().Send( GameMessages.CHAIN_RESET );
        }

        [Test]
        public void WhenChainEnded_ChainIsNull() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.EndChain();

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenChainEnded_ChainResetEventIsSent() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.EndChain();

            MyMessenger.Received().Send( GameMessages.CHAIN_RESET );
        }

        [Test]
        public void WhenChainEnded_ChainIsProcessed() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.EndChain();

            ChainProcessor.Received().Process( Arg.Any<List<IGamePiece>>() );
        }

        private ChainManager CreateChainManager_WithNoChain() {
            systemUnderTest.Chain = null;

            return systemUnderTest;
        }

        private ChainManager CreateChainManager_WithEmptyActiveChain() {
            systemUnderTest.Chain = new List<IGamePiece>();

            return systemUnderTest;
        }
    }
}
