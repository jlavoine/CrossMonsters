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
    }
}
