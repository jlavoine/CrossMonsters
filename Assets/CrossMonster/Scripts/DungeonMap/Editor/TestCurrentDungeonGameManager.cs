using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestCurrentDungeonGameManager : ZenjectUnitTestFixture {

        [Inject]
        IMonsterDataManager MockMonsterData;

        [Inject]
        IDungeonRewardSpawner MockRewardSpawner;

        [Inject]
        CurrentDungeonGameManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterDataManager>().FromInstance( Substitute.For<IMonsterDataManager>() );
            Container.Bind<IDungeonRewardSpawner>().FromInstance( Substitute.For<IDungeonRewardSpawner>() );
            Container.Bind<CurrentDungeonGameManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenSettingData_DataIsSaved() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetMonsters().Returns( new List<string>() { "a", "b", "c" } );
            MockMonsterData.GetData( Arg.Any<string>() ).Returns( Substitute.For<IMonsterData>() );
            systemUnderTest.SetData( mockData );

            Assert.AreEqual( mockData, systemUnderTest.Data );
        }

        [Test]
        public void WhenSettingData_MonstersAreGenerated() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetMonsters().Returns( new List<string>() { "a", "b", "c" } );
            MockMonsterData.GetData( Arg.Any<string>() ).Returns( Substitute.For<IMonsterData>() );

            systemUnderTest.SetData( mockData );

            Assert.AreEqual( 3, systemUnderTest.Monsters.Count );
        }

        [Test]
        public void WhenSettingData_RewardsAreGenerated() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetRewards().Returns( new List<IGameRewardData>() { Substitute.For<IGameRewardData>() } );

            systemUnderTest.SetData( mockData );

            Assert.AreEqual( 1, systemUnderTest.Rewards.Count );
        }

        [Test]
        public void WhenAwarding_AllDungeonRewardsAreAwarded() {
            List<IDungeonReward> mockRewards = new List<IDungeonReward>();
            mockRewards.Add( Substitute.For<IDungeonReward>() );
            mockRewards.Add( Substitute.For<IDungeonReward>() );
            mockRewards.Add( Substitute.For<IDungeonReward>() );

            systemUnderTest.Rewards = mockRewards;
            systemUnderTest.AwardRewards();

            foreach ( IDungeonReward reward in mockRewards ) {
                reward.Received().Award();
            }
        }
    }
}