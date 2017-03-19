using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestPlayerDataManager : ZenjectUnitTestFixture {
        [Inject]
        PlayerDataManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<PlayerDataManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_GetsExpectedVirtualCurrency() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetVirtualCurrency( PlayerDataManager.GOLD_KEY, Arg.Any<Callback<int>>() );
        }
    }
}