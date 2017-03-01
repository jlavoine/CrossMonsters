using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameBackgroundPM : CrossMonstersUnitTest {
        [Test]
        public void EnteringBackground_CancelsChain() {
            GameBackgroundPM systemUnderTest = new GameBackgroundPM();

            systemUnderTest.PlayerEnteredBackground();

            ChainManager.Instance.Received().CancelChain();
        }
    }
}
