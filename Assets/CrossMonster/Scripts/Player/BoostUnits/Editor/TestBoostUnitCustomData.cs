using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestBoostUnitCustomData : ZenjectUnitTestFixture {

        [Test]
        public void HasEffect_ReturnsTrue_IfEffectInDictionary() {
            BoostUnitCustomData systemUnderTest = new BoostUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>() { { "TestEffect", 1 } };

            Assert.IsTrue( systemUnderTest.HasEffect( "TestEffect" ) );
        }

        [Test]
        public void HasEffect_ReturnsFalse_IfEffectNotInDictionary() {
            BoostUnitCustomData systemUnderTest = new BoostUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>();

            Assert.IsFalse( systemUnderTest.HasEffect( "TestEffect" ) );
        }

        [Test]
        public void GetEffect_ReturnsEffectValue_IfEffectInDictionary() {
            BoostUnitCustomData systemUnderTest = new BoostUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>() { { "TestEffect", 1 } };

            Assert.AreEqual( 1, systemUnderTest.GetEffect( "TestEffect" ) );
        }

        [Test]
        public void GetEffect_ReturnsZero_IfEffectNotInDictionary() {
            BoostUnitCustomData systemUnderTest = new BoostUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>();

            Assert.AreEqual( 0, systemUnderTest.GetEffect( "TestEffect" ) );
        }
    }
}
