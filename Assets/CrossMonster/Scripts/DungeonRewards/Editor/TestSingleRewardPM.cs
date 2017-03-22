using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestSingleRewardPM : ZenjectUnitTestFixture {

        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        IAllRewardsPM MockRewardsPM;

        [Inject]
        SingleRewardPM.Factory systemFactory;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<IAllRewardsPM>().FromInstance( Substitute.For<IAllRewardsPM>() );
            Container.BindFactory<IDungeonRewardData, IAllRewardsPM, SingleRewardPM, SingleRewardPM.Factory>();            
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_CoverIsVisibleByDefault() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonRewardData>(), MockRewardsPM );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenUncovered_CoverVisibility_IsFalse() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonRewardData>(), MockRewardsPM );
            systemUnderTest.ViewModel.SetProperty( SingleRewardPM.COVER_VISIBLE_PROPERTY, true );

            systemUnderTest.UncoverReward();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenUncovered_AllRewardsPM_IsNotified() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonRewardData>(), MockRewardsPM );

            systemUnderTest.UncoverReward();

            MockRewardsPM.Received().RewardUncovered();
        }

        [Test]
        public void WhenCreating_CountProperty_EqualsDungeonRewardCount() {
            IDungeonRewardData mockData = Substitute.For<IDungeonRewardData>();
            mockData.GetCount().Returns( 101 );

            SingleRewardPM systemUnderTest = systemFactory.Create( mockData, MockRewardsPM );

            Assert.AreEqual( "101", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.COUNT_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_NameProperty_AsExpected() {
            IDungeonRewardData mockData = Substitute.For<IDungeonRewardData>();
            mockData.GetNameKey().Returns( "Test" );
            MockStringTable.Get( "Test" ).Returns( "MyTestItem" );

            SingleRewardPM systemUnderTest = systemFactory.Create( mockData, MockRewardsPM );

            Assert.AreEqual( "MyTestItem", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.NAME_PROPERTY ) );
        }
    }
}