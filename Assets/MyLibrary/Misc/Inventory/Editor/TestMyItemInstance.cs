using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestMyItemInstance : ZenjectUnitTestFixture {

        [Test]
        public void WhenRemovingUsesFromInstance_CountIsChanged() {
            MyItemInstance systemUnderTest = new MyItemInstance();
            systemUnderTest.Count = 10;
            systemUnderTest.RemoveUses( 3 );

            Assert.AreEqual( 7, systemUnderTest.Count );
        }

        [Test]
        public void WhenRemovingUsesFromInstance_CountDoesNotGoBelowZero() {
            MyItemInstance systemUnderTest = new MyItemInstance();
            systemUnderTest.Count = 10;
            systemUnderTest.RemoveUses( 30 );

            Assert.AreEqual( 0, systemUnderTest.Count );
        }

        [Test]
        public void WhenCatalogItemHasTag_HasTag_ReturnsTrue() {
            IMyCatalogItem mockCatalogItem = Substitute.For<IMyCatalogItem>();
            mockCatalogItem.HasTag( "SomeTag" ).Returns( true );

            MyItemInstance systemUnderTest = new MyItemInstance();
            systemUnderTest.SetCatalogItem( mockCatalogItem );

            Assert.IsTrue( systemUnderTest.HasTag( "SomeTag" ) );
        }

        [Test]
        public void WhenCatalogItemDoesNotHaveTag_HasTag_ReturnsFalse() {
            IMyCatalogItem mockCatalogItem = Substitute.For<IMyCatalogItem>();
            mockCatalogItem.HasTag( "SomeTag" ).Returns( false );

            MyItemInstance systemUnderTest = new MyItemInstance();
            systemUnderTest.SetCatalogItem( mockCatalogItem );

            Assert.IsFalse( systemUnderTest.HasTag( "SomeTag" ) );
        }
    }
}