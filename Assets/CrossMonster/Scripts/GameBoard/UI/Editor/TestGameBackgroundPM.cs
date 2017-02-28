using NUnit.Framework;
using NSubstitute;
using Zenject;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGameBackgroundPM : ZenjectUnitTestFixture {
        [Inject]
        IChainManager ChainManager;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainManager>().To<ChainManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void EnteringBackground_CancelsChain() {
            GameBackgroundPM systemUnderTest = new GameBackgroundPM();

            systemUnderTest.PlayerEnteredBackground();

            ChainManager.Received().CancelChain();
        }
    }
}
