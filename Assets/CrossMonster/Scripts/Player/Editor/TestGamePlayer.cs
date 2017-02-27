using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePlayer : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePlayer systemUnderTest = new GamePlayer( Substitute.For<IPlayerData>() );

            MyMessenger.Instance.Received().AddListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePlayer systemUnderTest = new GamePlayer( Substitute.For<IPlayerData>() );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void PlayerHP_MatchesData() {
            IPlayerData mockData = Substitute.For<IPlayerData>();
            mockData.GetHP().Returns( 101 );
            GamePlayer systemUnderTest = new GamePlayer( mockData );

            Assert.AreEqual( 101, systemUnderTest.HP );
        }

        [Test]
        public void GetDefenseForType_ReturnsExpected() {
            IPlayerData mockData = Substitute.For<IPlayerData>();
            mockData.GetDefenseForType( 0 ).Returns( 100 );

            GamePlayer systemUnderTest = new GamePlayer( mockData );

            Assert.AreEqual( 100, systemUnderTest.GetDefenseForType( 0 ) );
        }

        static object[] GetDamageFromMonsterTests = {
            new object[] { 100, 0, 100 },
            new object[] { 100, 50, 50 },
            new object[] { 100, 1000, 1 },
        };

        [Test, TestCaseSource("GetDamageFromMonsterTests")]
        public void GetDamageFromMonster_ReturnsAsExpected( int i_monsterAttack, int i_playerDefenseForType, int i_expectedDamage ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.AttackPower.Returns( i_monsterAttack );

            IPlayerData mockPlayerData = Substitute.For<IPlayerData>();
            mockPlayerData.GetDefenseForType( Arg.Any<int>() ).Returns( i_playerDefenseForType );

            GamePlayer systemUnderTest = new GamePlayer( mockPlayerData );

            int damageTaken = systemUnderTest.GetDamageFromMonster( mockMonster );
            Assert.AreEqual( i_expectedDamage, damageTaken );
        } 
    }
}