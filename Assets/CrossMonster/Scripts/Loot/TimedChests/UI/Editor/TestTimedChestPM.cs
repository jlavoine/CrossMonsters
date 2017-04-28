using NUnit.Framework;
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
        private IPlayerInventoryManager MockInventory;
        private ITimedChestData MockData;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockInventory = Substitute.For<IPlayerInventoryManager>();
            MockData = Substitute.For<ITimedChestData>();   
        }

        [Test]
        public void WhenCreated_NameProperty_IsExpected() {
            MockStringTable.Get( "TestNameKey" ).Returns( "TestName" );
            MockData.GetNameKey().Returns( "TestNameKey" );

            TimedChestPM systemUnderTest = new TimedChestPM( MockStringTable, MockInventory, MockData );

            Assert.AreEqual( "TestName", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.NAME_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_KeyProgress_IsExpected() {
            MockData.GetKeysRequired().Returns( 10 );
            MockInventory.GetItemCount( Arg.Any<string>() ).Returns( 3 );

            TimedChestPM systemUnderTest = new TimedChestPM( MockStringTable, MockInventory, MockData );

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.CURRENT_KEYS_PROPERTY ) );
            Assert.AreEqual( "10", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.REQUIRED_KEYS_PROPERTY ) );
        }
    }
}
