using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestChainManager : CrossMonstersUnitTest {

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            //ChainManager systemUnderTest = new ChainManager();
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            ChainManager systemUnderTest = new ChainManager();

            systemUnderTest.Dispose();
        }

        static object[] IsNoChainTests = {
            new object[] { null, true },
            new object[] { new List<IGamePiece>(), false },
            new object[] { new List<IGamePiece>() { Substitute.For<IGamePiece>() }, false }
        };

        [Test, TestCaseSource("IsNoChainTests")]
        public void IsNoChain_ReturnsAsExpected( List<IGamePiece> i_chain, bool i_expected ) {
            ChainManager systemUnderTest = new ChainManager();
            systemUnderTest.Chain = i_chain;

            Assert.AreEqual( i_expected, systemUnderTest.IsNoChain() );
        }

        [Test]
        public void WhenNoChain_StartChain_CreatesChain() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();

            systemUnderTest.StartChain( Substitute.For<IGamePiece>() );

            Assert.IsNotNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenAlreadyChain_StartChain_DoesNotChangeChain() {
            List<IGamePiece> mockChain = new List<IGamePiece>();
            ChainManager systemUnderTest = new ChainManager();
            systemUnderTest.Chain = mockChain;

            systemUnderTest.StartChain( Substitute.For<IGamePiece>() );

            Assert.AreEqual( mockChain, systemUnderTest.Chain );
        }

        [Test]
        public void WhenNoChain_StartChain_AddsPieceToChain() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();

            systemUnderTest.StartChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        [Test]
        public void WhenNoChain_StartChain_SendsMessageWithAddedPiece() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();

            systemUnderTest.StartChain( mockPiece );

            MyMessenger.Instance.Received().Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, mockPiece );
        }

        [Test]
        public void WhenNoChain_ContinuingChain_DoesNothing() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();

            systemUnderTest.ContinueChain( Substitute.For<IGamePiece>() );

            Assert.IsNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_AddPieceToChain() {
            ChainManager systemUnderTest = CreateChainManager_WithEmptyActiveChain();

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        [Test]
        public void WhenActiveChain_ContinuingChain_SendsPieceAddedEvent() {
            ChainManager systemUnderTest = CreateChainManager_WithEmptyActiveChain();

            IGamePiece mockPiece = Substitute.For<IGamePiece>();
            systemUnderTest.ContinueChain( mockPiece );

            MyMessenger.Instance.Received().Send<IGamePiece>( GameMessages.PIECE_ADDED_TO_CHAIN, mockPiece );
        }

        private ChainManager CreateChainManager_WithNoChain() {
            ChainManager systemUnderTest = new ChainManager();
            systemUnderTest.Chain = null;

            return systemUnderTest;
        }

        private ChainManager CreateChainManager_WithEmptyActiveChain() {
            ChainManager systemUnderTest = new ChainManager();
            systemUnderTest.Chain = new List<IGamePiece>();

            return systemUnderTest;
        }
    }
}
