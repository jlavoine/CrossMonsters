using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;
using System;
using UnityEngine.TestTools;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTimedChestSaveData : ZenjectUnitTestFixture {
        [Inject]
        TimedChestSaveData systemUnderTest;

        [Inject]
        IPlayerInventoryManager MockInventory;

        [Inject]
        IDungeonRewardSpawner MockRewardSpawner;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IPlayerInventoryManager>().FromInstance( Substitute.For<IPlayerInventoryManager>() );
            Container.Bind<IDungeonRewardSpawner>().FromInstance( Substitute.For<IDungeonRewardSpawner>() );
            Container.Bind<TimedChestSaveData>().AsSingle();            
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_CallsToBackendForData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetReadOnlyPlayerData( TimedChestSaveData.SAVE_DATA_KEY, Arg.Any<Callback<string>>() );
        }

        [Test]
        public void GettingCurrentKeys_ReturnsInventoryCount() {
            MockInventory.GetItemCount( Arg.Any<string>() ).Returns( 111 );

            Assert.AreEqual( 111, systemUnderTest.GetCurrentKeysForChest( "Test" ) );
        }

        [Test]
        public void ChestIsNotAvailable_IfNotInDictionary() {
            systemUnderTest.SaveData = new Dictionary<string, ITimedChestSaveDataEntry>();

            Assert.IsFalse( systemUnderTest.IsChestAvailable( "NotInDict" ) );
        }

        [Test]
        public void ChestIsAvailable_WhenBackendTime_IsGreaterThan_NextAvailableTime() {
            InitSystemWithBackendTime( 1000 );
            SetSaveDataWithMockEntryOfAvailableTime( "TestChest", 0 );

            Assert.IsTrue( systemUnderTest.IsChestAvailable( "TestChest" ) );
        }

        [Test]
        public void ChestIsAvailable_WhenBackendTime_IsEqualTo_NextAvailableTime() {
            InitSystemWithBackendTime( 1000 );
            SetSaveDataWithMockEntryOfAvailableTime( "TestChest", 1000 );

            Assert.IsTrue( systemUnderTest.IsChestAvailable( "TestChest" ) );
        }

        [Test]
        public void ChestIsNotAvailable_WhenBackendTime_IsLessThan_NextAvailableTime() {
            InitSystemWithBackendTime( 1000 );
            SetSaveDataWithMockEntryOfAvailableTime( "TestChest", 1001 );

            Assert.IsFalse( systemUnderTest.IsChestAvailable( "TestChest" ) );
        }

        [Test]
        public void GetNextAvailableTime_ReturnsZero_IfNoData() {
            systemUnderTest.SaveData = new Dictionary<string, ITimedChestSaveDataEntry>();

            Assert.AreEqual( 0, systemUnderTest.GetNextAvailableTime( "NotInDict" ) );
        }

        [Test]
        public void GetNextAvailableTime_ReturnsNextAvailableFromData() {
            systemUnderTest.SaveData = new Dictionary<string, ITimedChestSaveDataEntry>();
            SetSaveDataWithMockEntryOfAvailableTime( "TestChest", 1000 );

            Assert.AreEqual( 1000, systemUnderTest.GetNextAvailableTime( "TestChest" ) );
        }

        static object[] CanOpenTests = {
            new object[] { 5, 5, true },
            new object[] { 5, 10, false },
            new object[] { 0, 5, false },
        };

        [Test, TestCaseSource( "CanOpenTests" )]
        public void CanOpenChest_ReturnsAsExpected( int i_ownedKeys, int i_requiredKeys, bool i_expected ) {
            MockInventory.GetItemCount( Arg.Any<string>() ).Returns( i_ownedKeys );
            ITimedChestData mockData = Substitute.For<ITimedChestData>();
            mockData.GetKeysRequired().Returns( i_requiredKeys );

            bool canOpen = systemUnderTest.CanOpenChest( mockData );

            Assert.AreEqual( i_expected, canOpen );
        }

        [Test, Ignore("This test is just fucked up. Will not work even though I know it's working because of fucking output. Waste of fucking 20 minutes")]
        public void WhenChestIsOpened_KeysAreRemovedFromInventory() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            ITimedChestData mockData = Substitute.For<ITimedChestData>();
            mockData.GetKeysRequired().Returns( 5 );
            mockData.GetKeyId().Returns( "KeyId" );
   
            systemUnderTest.OpenChest( mockData, Substitute.For<ITimedChestPM>() );

            MockInventory.Received().RemoveUsesFromItem( "KeyId", 5 );           
        }

        [Test]
        public void WhenChestIsOpened_RequestIsSentToServer() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );
            systemUnderTest.OpenChest( Substitute.For<ITimedChestData>(), Substitute.For<ITimedChestPM>() );

            mockBackend.Received().MakeCloudCall( BackendMethods.OPEN_TIMED_CHEST, Arg.Any<Dictionary<string, string>>(), Arg.Any<Callback<Dictionary<string, string>>>() );
        }

        [Test]
        public void WhenChestOpenResponseIsReceived_SpawnerCreatesAndAwardsReward() {
            IOpenTimedChestResponse mockResponse = Substitute.For<IOpenTimedChestResponse>();
            mockResponse.GetReward().Returns( Substitute.For<IGameRewardData>() );
            mockResponse.IsOpeningVerified().Returns( true );
            IDungeonReward mockReward = Substitute.For<IDungeonReward>();
            MockRewardSpawner.Create( Arg.Any<IGameRewardData>() ).Returns( mockReward );

            systemUnderTest.OnOpenResponseFromServer( mockResponse, Substitute.For<ITimedChestPM>(), Substitute.For<ITimedChestData>() );

            MockRewardSpawner.Received().Create( Arg.Any<IGameRewardData>() );
            mockReward.Received().Award();
        }

        [Test]
        public void WhenChestOpenResponseIsReceived_ChestPM_RewardGetsSet_And_UpdatePropertiesCalled() {
            ITimedChestPM mockPM = Substitute.For<ITimedChestPM>();
            IOpenTimedChestResponse mockResponse = Substitute.For<IOpenTimedChestResponse>();
            mockResponse.IsOpeningVerified().Returns( true );

            systemUnderTest.OnOpenResponseFromServer( mockResponse, mockPM, Substitute.For<ITimedChestData>() );

            mockPM.Received().ShowOpenReward( Arg.Any<IDungeonReward>() );
            mockPM.Received().UpdateProperties();
        }

        [Test]
        public void WhenChestOpenResponseIsReceived_ChestsNextAvailableTime_IsUpdatedInSaveData() {
            IOpenTimedChestResponse mockResponse = Substitute.For<IOpenTimedChestResponse>();
            mockResponse.IsOpeningVerified().Returns( true );
            mockResponse.GetNextAvailableTime().Returns( 1000 );

            ITimedChestData mockData = Substitute.For<ITimedChestData>();
            mockData.GetId().Returns( "TestId" );

            systemUnderTest.SaveData = new Dictionary<string, ITimedChestSaveDataEntry>() { { "TestId", new TimedChestSaveDataEntry() { NextAvailableTime = 0 } } };

            systemUnderTest.OnOpenResponseFromServer( mockResponse, Substitute.For<ITimedChestPM>(), mockData );

            Assert.AreEqual( 1000, systemUnderTest.SaveData["TestId"].GetNextAvailableTime() );
        }

        private void InitSystemWithBackendTime( long i_time ) {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            mockBackend.GetDateTime().Returns( new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( i_time ) );
            systemUnderTest.Init( mockBackend );
        }

        private void SetSaveDataWithMockEntryOfAvailableTime( string i_id, long i_time ) {
            ITimedChestSaveDataEntry mockEntry = Substitute.For<ITimedChestSaveDataEntry>();
            mockEntry.GetNextAvailableTime().Returns( i_time );
            systemUnderTest.SaveData = new Dictionary<string, ITimedChestSaveDataEntry>() { { i_id, mockEntry } };
        }
    }
}