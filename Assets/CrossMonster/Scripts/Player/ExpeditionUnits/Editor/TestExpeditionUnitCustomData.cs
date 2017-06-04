using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestExpeditionUnitCustomData : ZenjectUnitTestFixture {

        [Test]
        public void HasEffect_ReturnsTrue_IfEffectInDictionary() {
            ExpeditionUnitCustomData systemUnderTest = new ExpeditionUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>() { { "TestEffect", 1 } };

            Assert.IsTrue( systemUnderTest.HasEffect( "TestEffect" ) );
        }

        [Test]
        public void HasEffect_ReturnsFalse_IfEffectNotInDictionary() {
            ExpeditionUnitCustomData systemUnderTest = new ExpeditionUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>();

            Assert.IsFalse( systemUnderTest.HasEffect( "TestEffect" ) );
        }

        [Test]
        public void GetEffect_ReturnsEffectValue_IfEffectInDictionary() {
            ExpeditionUnitCustomData systemUnderTest = new ExpeditionUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>() { { "TestEffect", 1 } };

            Assert.AreEqual( 1, systemUnderTest.GetEffect( "TestEffect" ) );
        }

        [Test]
        public void GetEffect_ReturnsZero_IfEffectNotInDictionary() {
            ExpeditionUnitCustomData systemUnderTest = new ExpeditionUnitCustomData();
            systemUnderTest.Effects = new Dictionary<string, int>();

            Assert.AreEqual( 0, systemUnderTest.GetEffect( "TestEffect" ) );
        }
    }
}
