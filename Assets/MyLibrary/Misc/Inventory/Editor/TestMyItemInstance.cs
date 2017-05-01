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
    }
}