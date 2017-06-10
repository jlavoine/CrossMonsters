using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestGamePlayer : ZenjectUnitTestFixture {

        IMessageService MockMessenger;
        IPlayerDataManager MockPlayerData;
        IDamageCalculator MockDamageCalculator;
        ICurrentBoostUnits MockBoostUnits;       

        [SetUp]
        public void CommonInstall() {
            MockMessenger = Substitute.For<IMessageService>();
            MockDamageCalculator = Substitute.For<IDamageCalculator>();
            MockPlayerData = Substitute.For<IPlayerDataManager>();
            MockBoostUnits = Substitute.For<ICurrentBoostUnits>();
        }

        private GamePlayer CreateSystem() {
            GamePlayer systemUnderTest = new GamePlayer( MockMessenger, MockBoostUnits, MockDamageCalculator, MockPlayerData );
            return systemUnderTest;
        }

        [Test]
        public void WhenCreating_MaxHP_MatchesPlayerDataManager() {
            MockPlayerData.GetStat( PlayerStats.HP ).Returns( 999 );
            GamePlayer systemUnderTest = CreateSystem();

            Assert.AreEqual( 999, systemUnderTest.MaxHP );
        }

        [Test]
        public void WhenCreate_MaxHP_IsEffectedByBoostUnits() {
            MockPlayerData.GetStat( PlayerStats.HP ).Returns( 999 );
            MockBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_HP ).Returns( 1 );
            GamePlayer systemUnderTest = CreateSystem();

            Assert.AreEqual( 1000, systemUnderTest.MaxHP );
        }

        [Test]
        public void WhenCreating_SubscribesToExpectedMessages() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.Initialize();

            MockMessenger.Received().AddListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToExpectedMessages() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.Initialize();
            systemUnderTest.Dispose();

            MockMessenger.Received().RemoveListener<IGameMonster>( GameMessages.MONSTER_ATTACK, Arg.Any<Callback<IGameMonster>>() );
        }

        [Test]
        public void GetDefenseForType_ReturnsExpected() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.PHY_DEF ).Returns( 100 );
            
            Assert.AreEqual( 100, systemUnderTest.GetDefenseForType( 0 ) );
        }

        [Test]
        public void GetDefenseForType_GetsBonus_FromBoostUnits() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.PHY_DEF ).Returns( 1 );
            MockBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_DEFENSE ).Returns( 5 );

            Assert.AreEqual( 6, systemUnderTest.GetDefenseForType( 0 ) );
        }

        [Test]
        public void GetAttackPowerForType_ReturnsExpected() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.PHY_ATK ).Returns( 100 );

            Assert.AreEqual( 100, systemUnderTest.GetAttackPowerForType( 0 ) );
        }

        [Test]
        public void GetAttackPowerForType_GetsBonus_FromBoostUnits() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.PHY_ATK ).Returns( 100 );
            MockBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_BONUS_DAMAGE ).Returns( 5 );

            Assert.AreEqual( 105, systemUnderTest.GetAttackPowerForType( 0 ) );
        }

        [Test]
        public void GetHpRegenPerWave_ReturnsExpected() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.WAVE_HP_REGEN ).Returns( 333 );

            Assert.AreEqual( 333, systemUnderTest.GetHpRegenPerWave() );
        }

        [Test]
        public void GetHpRegenPerWave_IsEffectedByBoostUnits() {
            GamePlayer systemUnderTest = CreateSystem();
            MockPlayerData.GetStat( PlayerStats.WAVE_HP_REGEN ).Returns( 333 );
            MockBoostUnits.GetEffectValue( BoostUnitKeys.PLAYER_HP_WAVE_REGEN ).Returns( 222 );

            Assert.AreEqual( 555, systemUnderTest.GetHpRegenPerWave() );
        }

        [Test]
        public void WhenAttacked_DamageIsSubstractedFromHP() {
            GamePlayer systemUnderTest = CreateSystem();
            MockDamageCalculator.GetDamageFromMonster( Arg.Any<IGameMonster>(), Arg.Any<IGamePlayer>() ).Returns( 10 );            
            systemUnderTest.HP = 100;
            systemUnderTest.MaxHP = 100;

            systemUnderTest.OnAttacked( Substitute.For<IGameMonster>() );

            Assert.AreEqual( 90, systemUnderTest.HP );
        }

        [Test]
        public void WhenHpIsAltered_UpdateMessageIsSent() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.AlterHP( 1 );

            MockMessenger.Received().Send( GameMessages.UPDATE_PLAYER_HP );
        }

        [Test]
        public void WhenRemovingHP_IfPlayerIsDead_MessageSent() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( -101 );

            MockMessenger.Received().Send( GameMessages.PLAYER_DEAD );
        }

        [Test]
        public void WhenRemovingHP_HpWontFallBelowZero() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( -101 );

            Assert.AreEqual( 0, systemUnderTest.HP );
        }

        [Test]
        public void WhenAlteringHP_HpWontGoAboveMax() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.MaxHP = 100;
            systemUnderTest.HP = 100;

            systemUnderTest.AlterHP( 100 );

            Assert.AreEqual( 100, systemUnderTest.HP );
        }

        [Test]
        public void OnWaveFinished_HpIsAdded_BasedOnRegenStat() {
            GamePlayer systemUnderTest = CreateSystem();
            systemUnderTest.HP = 100;
            systemUnderTest.MaxHP = 200;
            MockPlayerData.GetStat( PlayerStats.WAVE_HP_REGEN ).Returns( 50 );

            systemUnderTest.OnWaveFinished();

            Assert.AreEqual( 150, systemUnderTest.HP );
        }
    }
}