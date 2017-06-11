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

        [Test]
        public void WhenSettingIndex_KeyCountIsExpected() {
            IMyItemInstance mockKey = Substitute.For<IMyItemInstance>();
            mockKey.GetCount().Returns( 3 );
            MockInventory.GetGauntletKeysFromIndex( 1 ).Returns( mockKey );
            EnterGauntletPM systemUnderTest = CreateSystem();
            systemUnderTest.SetIndex( 1 );

            int keyCount = systemUnderTest.ViewModel.GetPropertyValue<int>( EnterGauntletPM.KEY_COUNT_PROPERTY );

            Assert.AreEqual( 3, keyCount );
        }
    }
}
