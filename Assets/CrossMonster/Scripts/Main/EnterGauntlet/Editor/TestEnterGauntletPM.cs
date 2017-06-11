using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestEnterGauntletPM : MonoBehaviour {

        private IGauntletInventoryHelper MockInventory;
        private IDungeonLoader MockDungeonLoader;

        [SetUp]
        public void CommonInstall() {
            MockInventory = Substitute.For<IGauntletInventoryHelper>();
            MockDungeonLoader = Substitute.For<IDungeonLoader>();
        }

        private EnterGauntletPM CreateSystem() {
            EnterGauntletPM systemUnderTest = new EnterGauntletPM( MockInventory, MockDungeonLoader );
            return systemUnderTest;
        }

        private IMyItemInstance CreateMockKeyWithCountForIndex( int i_count, int i_index ) {
            IMyItemInstance mockKey = Substitute.For<IMyItemInstance>();
            mockKey.GetCount().Returns( i_count );
            MockInventory.GetGauntletKeysFromIndex( i_index ).Returns( mockKey );

            return mockKey;
        }

        [Test]
        public void WhenSettingIndex_KeyCountIsExpected() {
            IMyItemInstance mockKey = CreateMockKeyWithCountForIndex( 3, 1 );
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.SetIndex( 1 );

            int keyCount = systemUnderTest.ViewModel.GetPropertyValue<int>( EnterGauntletPM.KEY_COUNT_PROPERTY );

            Assert.AreEqual( 3, keyCount );
        }

        [Test]
        public void CanEnterProperty_IsTrue_WhenKeyCount_GreaterThanZero() {
            CreateMockKeyWithCountForIndex( 1, 1 );
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.SetIndex( 1 );

            bool canEnter = systemUnderTest.ViewModel.GetPropertyValue<bool>( EnterGauntletPM.CAN_ENTER_GAUNTLET_PROPERTY );

            Assert.IsTrue( canEnter );
        }

        [Test]
        public void CanEnterProperty_IsFalse_WhenKeyCount_IsZero() {
            CreateMockKeyWithCountForIndex( 0, 1 );
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.SetIndex( 1 );

            bool canEnter = systemUnderTest.ViewModel.GetPropertyValue<bool>( EnterGauntletPM.CAN_ENTER_GAUNTLET_PROPERTY );

            Assert.IsFalse( canEnter );
        }

        [Test]
        public void WhenEnteringGauntlet_KeyIsConsumedFromInventory() {
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.Index = 0;

            systemUnderTest.EnterGauntlet( 0 );

            MockInventory.Received().ConsumeGauntletKeyForIndex( 0 );
        }

        [Test]
        public void WhenEnteringGauntlet_DungeonLoaderLoadsWithExpectedValues() {
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.Index = 0;

            systemUnderTest.EnterGauntlet( 2 );

            MockDungeonLoader.Received().LoadDungeon( EnterGauntletPM.GAUNTLET_GAME_TYPE, 0, 2 );
        }

        [Test]
        public void WhenEnteringGauntlet_SystemHidesItself() {
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( EnterGauntletPM.VISIBLE_PROPERTY, true );

            systemUnderTest.EnterGauntlet( 0 );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( EnterGauntletPM.VISIBLE_PROPERTY ) );
        }
    }
}
