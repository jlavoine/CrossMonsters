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

        [Test]
        public void WhenConsumingGauntletKey_ItemRemovedFromInventory() {
            GauntletInventoryHelper systemUnderTest = CreateSystem();

            systemUnderTest.ConsumeGauntletKeyForIndex( 0 );

            MockInventory.Received().RemoveUsesFromItem( "Gauntlet_Key_0", 1 );
        }

        [Test]
        public void GetItemIdKeyForIndex_ReturnsExpected() {
            string expectedKey = "Gauntlet_Key_0";
            GauntletInventoryHelper systemUnderTest = CreateSystem();

            string key = systemUnderTest.GetGauntletItemKeyForIndex( 0 );

            Assert.AreEqual( expectedKey, key );
        }
    }
}