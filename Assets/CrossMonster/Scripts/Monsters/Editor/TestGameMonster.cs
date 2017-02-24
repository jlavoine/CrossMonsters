using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestGameMonster {
        [Test]
        public void WhenCreating_RemainingHP_EqualsMaxHP() {
            IMonsterData mockData = Substitute.For<IMonsterData>();
            mockData.GetMaxHP().Returns( 101 );

            GameMonster systemUnderTest = new GameMonster( mockData );

            Assert.AreEqual( 101, systemUnderTest.RemainingHP );
        }

        [Test]
        public void WhenCreating_StatsSetToDataValues() {
            IMonsterData mockData = Substitute.For<IMonsterData>();
            mockData.GetDefense().Returns( 10 );
            mockData.GetDefenseType().Returns( 2 );

            GameMonster systemUnderTest = new GameMonster( mockData );

            Assert.AreEqual( 10, systemUnderTest.Defense );
            Assert.AreEqual( 2, systemUnderTest.DefenseType );
        }

        static object[] DamagedTestCases = {
            new object[] { 5, 0, 5 },
            new object[] { 5, 4, 1 },
            new object[] { 5, 5, 1 },
            new object[] { -1, 5, 1 }
        };

        [Test, TestCaseSource("DamagedTestCases")]
        public void WhenAttackedByPlayer_ExpectedDamageRemovedFromRemainingHP( int i_playerAttackPower, int i_monsterDefense, int i_expectedDamage ) {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.GetAttackPowerForType( Arg.Any<int>() ).Returns( i_playerAttackPower );

            IMonsterData mockMonsterData = Substitute.For<IMonsterData>();
            mockMonsterData.GetMaxHP().Returns( 100 );
            mockMonsterData.GetDefense().Returns( i_monsterDefense );

            GameMonster systemUnderTest = new GameMonster( mockMonsterData );

            systemUnderTest.AttackedByPlayer( mockPlayer );

            int expectedHP = 100 - i_expectedDamage;
            Assert.AreEqual( expectedHP, systemUnderTest.RemainingHP );
        }

        static object[] IsDeadTestCases = {
            new object[] { 0, true },
            new object[] { 1, false },
            new object[] { 10, false },
            new object[] { -1, true },
        };

        [Test, TestCaseSource("IsDeadTestCases")]
        public void IsDead_ReturnsExpected( int i_remainingHP, bool i_isDead ) {
            GameMonster systemUnderTest = new GameMonster( Substitute.For<IMonsterData>() );
            systemUnderTest.RemainingHP = i_remainingHP;

            Assert.AreEqual( i_isDead, systemUnderTest.IsDead() );
        }
    }
}