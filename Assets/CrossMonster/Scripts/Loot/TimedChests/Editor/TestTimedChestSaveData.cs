using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;
using System;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTimedChestSaveData : ZenjectUnitTestFixture {
        [Inject]
        TimedChestSaveData systemUnderTest;

        [Inject]
        IPlayerInventoryManager MockInventory;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<TimedChestSaveData>().AsSingle();
            Container.Bind<IPlayerInventoryManager>().FromInstance( Substitute.For<IPlayerInventoryManager>() );
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