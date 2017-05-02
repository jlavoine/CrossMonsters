using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestPlayerDataManager : ZenjectUnitTestFixture {
        [Inject]
        PlayerDataManager systemUnderTest;

        [Inject]
        IMessageService MockMessenger;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<PlayerDataManager>().AsSingle();
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_GetsExpectedVirtualCurrency() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetVirtualCurrency( PlayerDataManager.GOLD_KEY, Arg.Any<Callback<int>>() );
        }

        [Test]
        public void WhenIniting_RelevantStatDataDownloaded() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( PlayerDataManager.STATS_INFO_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetReadOnlyPlayerData( PlayerDataManager.PLAYER_STATS_KEY, Arg.Any<Callback<string>>() );
        }

        static object[] GetStatTests = {
            new object[] { 1, 0f, 0 },
            new object[] { 0, 1f, 0 },
            new object[] { 1, 1f, 1 },
            new object[] { 1, 100f, 100 },
            new object[] { 1, 0.5f, 1 },
            new object[] { 3, 0.5f, 2 },
            new object[] { 2, 5f, 10 },
        };

        [Test, TestCaseSource( "GetStatTests" )]
        public void GetStat_ReturnsExpected( int i_playerStatLevel, float i_statValuePerLevel, int i_expectedResult ) {
            IPlayerStatData mockPlayerStats = Substitute.For<IPlayerStatData>();
            mockPlayerStats.GetStatLevel( Arg.Any<string>() ).Returns( i_playerStatLevel );

            IStatInfoData mockStatInfo = Substitute.For<IStatInfoData>();
            mockStatInfo.GetValuePerLevel( Arg.Any<string>() ).Returns( i_statValuePerLevel );

            systemUnderTest.PlayerStatData = mockPlayerStats;
            systemUnderTest.StatInfoData = mockStatInfo;

            int statValue = systemUnderTest.GetStat( "AnyStat" );
            Assert.AreEqual( i_expectedResult, statValue );
        }

        [Test]
        public void WhenGoldIsSet_ViaProperty_MessageIsSent() {
            systemUnderTest.Gold = 100;

            MockMessenger.Received().Send( GameMessages.PLAYER_GOLD_CHANGED );
        }
    }
}