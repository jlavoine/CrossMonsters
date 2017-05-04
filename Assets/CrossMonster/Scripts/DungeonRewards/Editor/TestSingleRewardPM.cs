using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
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
            Container.BindFactory<IDungeonReward, IAllRewardsPM, SingleRewardPM, SingleRewardPM.Factory>();            
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_CoverIsVisibleByDefault() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonReward>(), MockRewardsPM );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenUncovered_CoverVisibility_IsFalse() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonReward>(), MockRewardsPM );
            systemUnderTest.ViewModel.SetProperty( SingleRewardPM.COVER_VISIBLE_PROPERTY, true );

            systemUnderTest.UncoverReward();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenUncovered_AllRewardsPM_IsNotified() {
            SingleRewardPM systemUnderTest = systemFactory.Create( Substitute.For<IDungeonReward>(), MockRewardsPM );

            systemUnderTest.UncoverReward();

            MockRewardsPM.Received().RewardUncovered();
        }

        [Test]
        public void WhenCreating_WithReward_PropertiesAsExpected() {
            IDungeonReward mockReward = GetMockReward( 101, "Test" );
            MockStringTable.Get( "Test" ).Returns( "MyTestItem" );

            SingleRewardPM systemUnderTest = systemFactory.Create( mockReward, MockRewardsPM );

            Assert.AreEqual( "101", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.COUNT_PROPERTY ) );
            Assert.AreEqual( "MyTestItem", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.NAME_PROPERTY ) );
        }

        [Test]
        public void WhenSettingReward_PropertiesAsExpected() {
            IDungeonReward mockReward = GetMockReward( 101, "Test" );
            MockStringTable.Get( "Test" ).Returns( "MyTestItem" );

            SingleRewardPM systemUnderTest = systemFactory.Create( null, null );
            systemUnderTest.SetReward( mockReward );

            Assert.AreEqual( "101", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.COUNT_PROPERTY ) );
            Assert.AreEqual( "MyTestItem", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleRewardPM.NAME_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_WithNullParams_NoErrors() {
            SingleRewardPM systemUnderTest = systemFactory.Create( null, null );
        }

        private IDungeonReward GetMockReward( int i_count, string i_name ) {
            IDungeonReward mockReward = Substitute.For<IDungeonReward>();
            mockReward.GetCount().Returns( i_count );
            mockReward.GetNameKey().Returns( i_name );

            return mockReward;
        }
    }
}