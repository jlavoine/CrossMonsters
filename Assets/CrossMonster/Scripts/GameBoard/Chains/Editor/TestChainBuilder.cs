using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestChainBuilder : ZenjectUnitTestFixture {

        [Inject]
        ChainBuilder systemUnderTest;

        [Inject]
        IChainProcessor ChainProcessor;

        [Inject]
        IChainValidator ChainValidator;

        [Inject]
        IAudioManager Audio;

        [Inject]
        IMessageService MyMessenger;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainProcessor>().FromInstance( Substitute.For<IChainProcessor>() );
            Container.Bind<IChainValidator>().FromInstance( Substitute.For<IChainValidator>() );
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IAudioManager>().FromInstance( Substitute.For<IAudioManager>() );
            Container.Bind<ChainBuilder>().AsSingle();
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
        public void WhenAddingToChain_SoundIsPlayed() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.AddPieceToChain( Substitute.For<IGamePiece>() );

            Audio.Received().PlayOneShot( CombatAudioKeys.ADD_TO_CHAIN );
        }

        [Test]
        public void WhenNoChain_ContinuingChain_DoesNothing() {
            CreateChainManager_WithNoChain();

            systemUnderTest.ContinueChain( Substitute.For<IGamePiece>() );

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_IfChainIsValid_AddPieceToChain() {
            CreateChainManager_WithEmptyActiveChain();
            ChainValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_IfChainIsValid_SendsPieceAddedEvent() {
            CreateChainManager_WithEmptyActiveChain();
            ChainValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            MyMessenger.Received().Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, mockPiece );
        }

        [Test]
        public void WhenContinuingChain_IfChainIsNotValid_ChainIsCanceled() {
            CreateChainManager_WithEmptyActiveChain();
            ChainValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );

            systemUnderTest.ContinueChain( Substitute.For<IGamePiece>() );

            MyMessenger.Received().Send( GameMessages.CHAIN_RESET );
        }

        [Test]
        public void WhenChainCanceled_ChainIsNull() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.CancelChain();

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenChainCanceled_PiecesInChain_AreFailed() {
            systemUnderTest.Chain = new List<IGamePiece>();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.Chain.Add( mockPiece );

            systemUnderTest.CancelChain();

            mockPiece.Received().PieceFailedMatch();
        }

        [Test]
        public void WhenChainCanceled_ChainResetEventIsSent() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.CancelChain();

            MyMessenger.Received().Send( GameMessages.CHAIN_RESET );
        }

        [Test]
        public void WhenChainCanceled_SoundIsPlayed() {
            CreateChainManager_WithEmptyActiveChain();

            systemUnderTest.CancelChain();

            Audio.Received().PlayOneShot( CombatAudioKeys.CHAIN_BROKEN );
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

        private ChainBuilder CreateChainManager_WithNoChain() {
            systemUnderTest.Chain = null;

            return systemUnderTest;
        }

        private ChainBuilder CreateChainManager_WithEmptyActiveChain() {
            systemUnderTest.Chain = new List<IGamePiece>();

            return systemUnderTest;
        }
    }
}
