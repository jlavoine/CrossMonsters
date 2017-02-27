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

        [Test]
        public void WhenAttacked_DamageIsSubstractedFromHP() {
            DamageCalculator.Instance.GetDamageFromMonster( Arg.Any<IGameMonster>(), Arg.Any<IGamePlayer>() ).Returns( 10 );
            GamePlayer systemUnderTest = new GamePlayer( Substitute.For<IPlayerData>() );
            systemUnderTest.HP = 100;

            systemUnderTest.OnAttacked( Substitute.For<IGameMonster>() );

            Assert.AreEqual( 90, systemUnderTest.HP );
        }
    }
}