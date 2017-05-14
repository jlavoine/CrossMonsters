using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestDungeonReward : ZenjectUnitTestFixture {

        IPlayerDataManager MockPlayerData;

        [SetUp]
        public void BeforeTest() {
            MockPlayerData = Substitute.For<IPlayerDataManager>();
        }

        [Test]
        public void BasicValues_EqualDataValues() {
            IGameRewardData mockData = Substitute.For<IGameRewardData>();
            mockData.GetCount().Returns( 100 );
            mockData.GetId().Returns( "FakeID" );
            mockData.GetLootType().Returns( "Gold" );

            DungeonReward systemUnderTest = new DungeonReward( MockPlayerData, mockData );

            Assert.AreEqual( 100, systemUnderTest.GetCount() );
            Assert.AreEqual( "FakeID", systemUnderTest.GetId() );
            Assert.AreEqual( LootTypes.Gold, systemUnderTest.GetLootType() );
        }

        [Test]
        public void GetNameKey_ReturnsAsExpected() {
            IGameRewardData mockData = Substitute.For<IGameRewardData>();
            mockData.GetId().Returns( "FakeID" );

            DungeonReward systemUnderTest = new DungeonReward( MockPlayerData, mockData );

            Assert.AreEqual( "FakeID" + DungeonReward.NAME_KEY, systemUnderTest.GetNameKey() );
        }

        [Test]
        public void WhenAwardingGold_CountIsAddedToPlayerGold() {
            MockPlayerData.Gold = 0;
            IGameRewardData mockData = Substitute.For<IGameRewardData>();
            mockData.GetCount().Returns( 101 );
            mockData.GetLootType().Returns( "Gold" );


            DungeonReward systemUnderTest = new DungeonReward( MockPlayerData, mockData );
            systemUnderTest.Award();

            Assert.AreEqual( 101, MockPlayerData.Gold );
        }
    }
}
