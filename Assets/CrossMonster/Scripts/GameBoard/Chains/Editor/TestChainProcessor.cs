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
    public class TestChainProcessor : ZenjectUnitTestFixture {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        ChainProcessor systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterManager>().FromInstance( Substitute.For<IMonsterManager>() );
            Container.Bind<IGamePlayer>().FromInstance( Substitute.For<IGamePlayer>() );
            Container.Bind<ChainProcessor>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenProcessingChain_MonsterManagerProcesses() {
            List<int> chain = new List<int>();

            systemUnderTest.Process( chain );

            MonsterManager.Received().ProcessPlayerMove( Arg.Any<IGamePlayer>(), chain );
        }
    }
}
