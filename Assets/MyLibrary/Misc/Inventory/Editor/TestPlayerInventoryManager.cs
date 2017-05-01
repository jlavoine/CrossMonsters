using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestPlayerInventoryManager : ZenjectUnitTestFixture {
        [Inject]
        PlayerInventoryManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<PlayerInventoryManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenInited_BackendCallToGetCatalog_IsMade() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetItemCatalog( Arg.Any<Callback<Dictionary<string,IMyCatalogItem>>>() );
        }

        [Test]
        public void WhenMissingItem_CountReturnsZero() {
            systemUnderTest.Inventory = new Dictionary<string, IMyItemInstance>();

            Assert.AreEqual( 0, systemUnderTest.GetItemCount( "NoItem" ) );
        }

        [Test]
        public void GetItemCount_ReturnsExpected() {
            IMyItemInstance mockItem = Substitute.For<IMyItemInstance>();
            mockItem.GetCount().Returns( 111 );

            systemUnderTest.Inventory = new Dictionary<string, IMyItemInstance>() { { "SomeItem", mockItem } };

            Assert.AreEqual( 111, systemUnderTest.GetItemCount( "SomeItem" ) );
        }

        [Test]
        public void WhenRemovingUsesFromItem_IfItemIsInInventory_UsesAreRemoved() {
            IMyItemInstance mockItem_1 = Substitute.For<IMyItemInstance>();
            IMyItemInstance mockItem_2 = Substitute.For<IMyItemInstance>();

            systemUnderTest.Inventory = new Dictionary<string, IMyItemInstance>() { { "Item_1", mockItem_1 }, { "Item_2", mockItem_2 } };
            systemUnderTest.RemoveUsesFromItem( "Item_1", 1 );

            mockItem_1.Received().RemoveUses( 1 );
            mockItem_2.DidNotReceive().RemoveUses( 1 );
        }

        [Test]
        public void WhenRemovingUsesFromItem_IfItemIsNotInInventory_NothingHappens() {
            IMyItemInstance mockItem_1 = Substitute.For<IMyItemInstance>();
            IMyItemInstance mockItem_2 = Substitute.For<IMyItemInstance>();

            systemUnderTest.Inventory = new Dictionary<string, IMyItemInstance>() { { "Item_1", mockItem_1 }, { "Item_2", mockItem_2 } };
            systemUnderTest.RemoveUsesFromItem( "Item_3", 1 );

            mockItem_1.DidNotReceive().RemoveUses( 1 );
            mockItem_2.DidNotReceive().RemoveUses( 1 );
        }
    }
}
