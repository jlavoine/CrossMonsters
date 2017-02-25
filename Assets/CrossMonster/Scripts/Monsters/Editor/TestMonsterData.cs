using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestMonsterData : CrossMonstersUnitTest {
        [Test]
        public void GetMethods_ReturnExpectedValues() {
            MonsterData systemUnderTest = new MonsterData();
            systemUnderTest.MaxHP = 100;
            systemUnderTest.Id = "TestID";
            systemUnderTest.Damage = 10;
            systemUnderTest.DamageType = 1;
            systemUnderTest.DefenseType = 2;
            systemUnderTest.AttackRate = 1500;
            systemUnderTest.Defense = 11;

            Assert.AreEqual( 100, systemUnderTest.GetMaxHP() );
            Assert.AreEqual( "TestID", systemUnderTest.GetId() );
            Assert.AreEqual( 10, systemUnderTest.GetDamage() );
            Assert.AreEqual( 1, systemUnderTest.GetDamageType() );
            Assert.AreEqual( 2, systemUnderTest.GetDefenseType() );
            Assert.AreEqual( 1500, systemUnderTest.GetAttackRate() );
            Assert.AreEqual( 11, systemUnderTest.GetDefense() );
        }
    }
}
