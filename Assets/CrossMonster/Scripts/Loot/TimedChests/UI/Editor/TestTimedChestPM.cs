﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestTimedChestPM : ZenjectUnitTestFixture {

        private IStringTableManager MockStringTable;
        private ITimedChestSaveData MockSaveData;
        private ITimedChestData MockData;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockSaveData = Substitute.For<ITimedChestSaveData>();
            MockData = Substitute.For<ITimedChestData>();   
        }

        [Test]
        public void WhenCreated_NameProperty_IsExpected() {
            MockStringTable.Get( "TestNameKey" ).Returns( "TestName" );
            MockData.GetNameKey().Returns( "TestNameKey" );

            TimedChestPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "TestName", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.NAME_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_KeyProgress_IsExpected() {
            MockData.GetKeysRequired().Returns( 10 );
            MockSaveData.GetCurrentKeysForChest( Arg.Any<string>() ).Returns( 3 );

            TimedChestPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.CURRENT_KEYS_PROPERTY ) );
            Assert.AreEqual( "10", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.REQUIRED_KEYS_PROPERTY ) );
        }

        [Test]
        public void WhenChestIsUnavailable_AvailablePropertiesAsExpected() {
            MockSaveData.IsChestAvailable( Arg.Any<string>() ).Returns( false );

            TimedChestPM systemUnderTest = CreateSystem();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( TimedChestPM.AVAILABLE_PROPERTY ) );
            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( TimedChestPM.UNAVAILABLE_PROPERTY ) );
        }

        [Test]
        public void WhenChestIsAvailable_AvailablePropertiesAsExpected() {
            MockSaveData.IsChestAvailable( Arg.Any<string>() ).Returns( true );

            TimedChestPM systemUnderTest = CreateSystem();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( TimedChestPM.AVAILABLE_PROPERTY ) );
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( TimedChestPM.UNAVAILABLE_PROPERTY ) );
        }

        private TimedChestPM CreateSystem() {
            return new TimedChestPM( MockStringTable, MockSaveData, MockData );
        }
    }
}
