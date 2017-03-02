using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePlayer : ZenjectUnitTestFixture {

        [Inject]
        IMessageService Messenger;

        [Inject]
        IDamageCalculator DamageCalculator;

        [Inject]
        GamePlayer systemUnderTest;

        [Inject]
        IPlayerData PlayerData;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IDamageCalculator>().FromInstance( Substitute.For<IDamageCalculator>() );
            Container.Bind<IPlayerData>().FromInstance( Substitute.For<IPlayerData>() );
            Container.Bind<GamePlayer>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            systemUnderTest.Initialize();

            Messenger.Received().AddListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            systemUnderTest.Initialize();
            systemUnderTest.Dispose();

            Messenger.Received().RemoveListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void GetDefenseForType_ReturnsExpected() {
            PlayerData.GetDefenseForType( 0 ).Returns( 100 );
            
            Assert.AreEqual( 100, systemUnderTest.GetDefenseForType( 0 ) );
        }

        [Test]
        public void WhenAttacked_DamageIsSubstractedFromHP() {
            DamageCalculator.GetDamageFromMonster( Arg.Any<IGameMonster>(), Arg.Any<IGamePlayer>() ).Returns( 10 );            
            systemUnderTest.HP = 100;

            systemUnderTest.OnAttacked( Substitute.For<IGameMonster>() );

            Assert.AreEqual( 90, systemUnderTest.HP );
        }

        [Test]
        public void WhenAttacked_UpdateMessageIsSent() {
            DamageCalculator.GetDamageFromMonster( Arg.Any<IGameMonster>(), Arg.Any<IGamePlayer>() ).Returns( 10 );

            systemUnderTest.OnAttacked( Substitute.For<IGameMonster>() );

            Messenger.Received().Send( GameMessages.UPDATE_PLAYER_HP );
        }

        [Test]
        public void WhenRemovingHP_IfPlayerIsDead_MessageSent() {
            systemUnderTest.HP = 100;

            systemUnderTest.RemoveHP( 101 );

            Messenger.Received().Send( GameMessages.PLAYER_DEAD );
        }

        [Test]
        public void WhenRemovingHP_HpWontFallBelowZero() {
            systemUnderTest.HP = 100;

            systemUnderTest.RemoveHP( 101 );

            Assert.AreEqual( 0, systemUnderTest.HP );
        }
    }
}