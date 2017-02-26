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
    }
}
