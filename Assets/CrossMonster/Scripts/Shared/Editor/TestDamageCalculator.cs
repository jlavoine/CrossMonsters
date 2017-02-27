using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestDamageCalculator : CrossMonstersUnitTest {
        static object[] GetDamageFromMonsterTests = {
            new object[] { 100, 0, 100 },
            new object[] { 100, 50, 50 },
            new object[] { 100, 1000, 1 },
        };

        [Test, TestCaseSource( "GetDamageFromMonsterTests" )]
        public void GetDamageFromMonster_ReturnsAsExpected( int i_monsterAttack, int i_playerDefenseForType, int i_expectedDamage ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.AttackPower.Returns( i_monsterAttack );

            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.GetDefenseForType( Arg.Any<int>() ).Returns( i_playerDefenseForType );

            DamageCalculator systemUnderTest = new DamageCalculator();

            int damageTaken = systemUnderTest.GetDamageFromMonster( mockMonster, mockPlayer );
            Assert.AreEqual( i_expectedDamage, damageTaken );
        }
    }
}