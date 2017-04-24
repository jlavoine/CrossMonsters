using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTreasureDataManager : ZenjectUnitTestFixture {

        [Inject]
        TreasureDataManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {            
            Container.Bind<TreasureDataManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_CallsToBackendForData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( TreasureDataManager.TREASURE_DATA_TITLE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetTitleData( TreasureDataManager.TREASURE_SETS_TITLE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetReadOnlyPlayerData( TreasureDataManager.TREASURE_PROGRESS_KEY, Arg.Any<Callback<string>>() );
        }
    }
}
