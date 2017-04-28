using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTimedChestDataManager : ZenjectUnitTestFixture {
        [Inject]
        TimedChestDataManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<TimedChestDataManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_CallsToBackendForData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( TimedChestDataManager.TITLE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetReadOnlyPlayerData( TimedChestDataManager.SAVE_DATA_KEY, Arg.Any<Callback<string>>() );
        }
    }
}