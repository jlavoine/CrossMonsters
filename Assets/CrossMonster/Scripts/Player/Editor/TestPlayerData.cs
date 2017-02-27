using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestPlayerData : CrossMonstersUnitTest {
        [Test]
        public void GetHP_ReturnsExpectedValue() {
            PlayerData systemUnderTest = new PlayerData();
            systemUnderTest.HP = 100;

            Assert.AreEqual( 100, systemUnderTest.GetHP() );
        }

        [Test]
        public void GetDefenseForType_ReturnsExpected_WhenHasDefenseType() {
            PlayerData systemUnderTest = new PlayerData();
            systemUnderTest.Defenses = new Dictionary<int, int>() { { 0, 100 } };

            Assert.AreEqual( 100, systemUnderTest.GetDefenseForType( 0 ) );
        }

        [Test]
        public void GetDefenseForType_ReturnsExpected_WhenDoesNotHaveDefenseType() {
            PlayerData systemUnderTest = new PlayerData();
            systemUnderTest.Defenses = new Dictionary<int, int>() { { 0, 100 } };

            Assert.AreEqual( 0, systemUnderTest.GetDefenseForType( 1 ) );
        }
    }
}
