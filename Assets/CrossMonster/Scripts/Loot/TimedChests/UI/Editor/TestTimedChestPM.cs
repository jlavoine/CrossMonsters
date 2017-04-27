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

        [Test]
        public void WhenCreated_NameProperty_IsExpected() {
            IStringTableManager mockStringTable = Substitute.For<IStringTableManager>();
            mockStringTable.Get( "TestNameKey" ).Returns( "TestName" );
            ITimedChestData mockData = Substitute.For<ITimedChestData>();
            mockData.GetNameKey().Returns( "TestNameKey" );

            TimedChestPM systemUnderTest = new TimedChestPM( mockStringTable, mockData );

            Assert.AreEqual( "TestName", systemUnderTest.ViewModel.GetPropertyValue<string>( TimedChestPM.NAME_PROPERTY ) );
        }
    }
}
