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

        [SetUp]
        public void CommonInstall() {
            MockInventory = Substitute.For<IGauntletInventoryHelper>();
        }

        private EnterGauntletPM CreateSystem() {
            EnterGauntletPM systemUnderTest = new EnterGauntletPM( MockInventory );
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
    }
}
