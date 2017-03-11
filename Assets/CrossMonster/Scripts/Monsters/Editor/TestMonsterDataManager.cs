using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestMonsterDataManager : ZenjectUnitTestFixture {
        [Inject]
        MonsterDataManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<MonsterDataManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_CallsToBackendForData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( MonsterDataManager.MONSTER_DATA_TITLE_KEY, Arg.Any<Callback<string>>() );            
        }
    }
}
