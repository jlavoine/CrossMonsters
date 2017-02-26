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
            ChainManager systemUnderTest = new ChainManager();

            MyMessenger.Instance.Received().AddListener<IGamePiece>( GameMessages.START_CHAIN, Arg.Any<Callback<IGamePiece>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            ChainManager systemUnderTest = new ChainManager();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGamePiece>( GameMessages.START_CHAIN, Arg.Any<Callback<IGamePiece>>() );
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

            Assert.AreEqual( i_expected, systemUnderTest.IsNoCurrentChain() );
        }

        [Test]
        public void WhenNoChain_StartChain_CreatesChain() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();

            systemUnderTest.StartChain( Substitute.For<IGamePiece>() );

            Assert.IsNotNull( systemUnderTest.Chain );
        }

        [Test]
        public void WhenNoChain_StartChain_AddsPieceToChain() {
            ChainManager systemUnderTest = CreateChainManager_WithNoChain();
            IGamePiece mockPiece = Substitute.For<IGamePiece>();

            systemUnderTest.StartChain( mockPiece );

            Assert.Contains( mockPiece, systemUnderTest.Chain );
        }

        private ChainManager CreateChainManager_WithNoChain() {
            ChainManager systemUnderTest = new ChainManager();
            systemUnderTest.Chain = null;

            return systemUnderTest;
        }
    }
}
