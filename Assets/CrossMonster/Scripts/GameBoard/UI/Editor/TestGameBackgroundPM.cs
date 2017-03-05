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
        IChainBuilder ChainManager;

        [Inject]
        GameBackgroundPM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainBuilder>().FromInstance( Substitute.For<IChainBuilder>() );
            Container.Bind<GameBackgroundPM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void EnteringBackground_CancelsChain() {
            systemUnderTest.PlayerEnteredBackground();

            ChainManager.Received().CancelChain();
        }
    }
}
