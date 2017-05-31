using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestMyItemCatalog : ZenjectUnitTestFixture {

        [Test]
        public void WhenCatalogDoesNotContainItem_GetReturnsNull() {
            MyItemCatalog systemUnderTest = new MyItemCatalog( new Dictionary<string, IMyCatalogItem>() );           

            Assert.IsNull( systemUnderTest.GetItem( "SomeId" ) );
        }

        [Test]
        public void WhenCatalogContainsItem_GetReturnsItem() {
            IMyCatalogItem mockItem = Substitute.For<IMyCatalogItem>();
            MyItemCatalog systemUnderTest = new MyItemCatalog( new Dictionary<string, IMyCatalogItem>() { { "SomeId", mockItem } } );

            Assert.AreEqual( mockItem, systemUnderTest.GetItem( "SomeId" ) );
        }
    }
}
