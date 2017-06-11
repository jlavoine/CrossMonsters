using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestGauntletInventoryHelper : MonoBehaviour {

        private IPlayerInventoryManager MockInventory;

        [SetUp]
        public void CommonInstall() {
            MockInventory = Substitute.For<IPlayerInventoryManager>();
        }

        private GauntletInventoryHelper CreateSystem() {
            GauntletInventoryHelper systemUnderTest = new GauntletInventoryHelper( MockInventory );
            return systemUnderTest;
        }

        [Test]
        public void GetKeysFromIndex_ReturnsExpectedItemFromInventory() {
            string expectedKey = GauntletInventoryHelper.ITEM_KEY + "1";
            GauntletInventoryHelper systemUnderTest = CreateSystem();

            systemUnderTest.GetGauntletKeysFromIndex( 1 );

            MockInventory.Received().GetItem( expectedKey );
        }
    }
}