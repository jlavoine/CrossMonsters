using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestGamePlayer : ZenjectUnitTestFixture {

        [Inject]
        IMessageService Messenger;

        [Inject]
        IDamageCalculator DamageCalculator;

        [Inject]
        GamePlayer systemUnderTest;

        [Inject]
        IPlayerDataManager PlayerDataManager;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IDamageCalculator>().FromInstance( Substitute.For<IDamageCalculator>() );
            Container.Bind<IPlayerDataManager>().FromInstance( Substitute.For<IPlayerDataManager>() );
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
            PlayerDataManager.GetStat( PlayerStats.PHY_DEF ).Returns( 100 );
            
            Assert.AreEqual( 100, systemUnderTest.GetDefenseForType( 0 ) );
        }

        [Test]
        public void GetAttackPowerForType_ReturnsExpected() {
            PlayerDataManager.GetStat( PlayerStats.PHY_ATK ).Returns( 100 );

            Assert.AreEqual( 100, systemUnderTest.GetAttackPowerForType( 0 ) );
        }

        [Test]
        public void GetHpRegenPerWave_ReturnsExpected() {
            PlayerDataManager.GetStat( PlayerStats.WAVE_HP_REGEN ).Returns( 333 );

            Assert.AreEqual( 333, systemUnderTest.GetHpRegenPerWave() );
        }

        [Test]
        public void WhenAttacked_DamageIsSubstractedFromHP() {
            DamageCalculator.GetDamageFromMonster( Arg.Any<IGameMonster>(), Arg.Any<IGamePlayer>() ).Returns( 10 );            
            systemUnderTest.HP = 100;
            systemUnderTest.MaxHP = 100;

            systemUnderTest.OnAttacked( Substitute.For<IGameMonster>() );

            Assert.AreEqual( 90, systemUnderTest.HP );
        }

        [Test]
        public void WhenHpIsAltered_UpdateMessageIsSent() {            
            systemUnderTest.AlterHP( 1 );

            Messenger.Received().Send( GameMessages.UPDATE_PLAYER_HP );
        }

        [Test]
        public void WhenRemovingHP_IfPlayerIsDead_MessageSent() {
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( -101 );

            Messenger.Received().Send( GameMessages.PLAYER_DEAD );
        }

        [Test]
        public void WhenRemovingHP_HpWontFallBelowZero() {
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( -101 );

            Assert.AreEqual( 0, systemUnderTest.HP );
        }

        [Test]
        public void WhenAlteringHP_HpWontGoAboveMax() {
            systemUnderTest.MaxHP = 100;
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( 100 );

            Assert.AreEqual( 100, systemUnderTest.HP );
        }

        [Test]
        public void OnWaveFinished_HpIsAdded_BasedOnRegenStat() {
            systemUnderTest.HP = 100;
            systemUnderTest.MaxHP = 200;
            PlayerDataManager.GetStat( PlayerStats.WAVE_HP_REGEN ).Returns( 50 );

            systemUnderTest.OnWaveFinished();

            Assert.AreEqual( 150, systemUnderTest.HP );
        }
    }
}