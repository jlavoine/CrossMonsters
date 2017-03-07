using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestTreasureSetPM : CrossMonstersUnitTest {

        [Test]
        public void WhenCreating_NameProperty_SetAsExpected() {
            ITreasureSetData mockData = Substitute.For<ITreasureSetData>();
            mockData.GetId().Returns( "Test" );
            StringTableManager.Instance.Get( "Test_Name" ).Returns( "FakeName" );

            TreasureSetPM systemUnderTest = new TreasureSetPM( mockData );

            Assert.AreEqual( "FakeName", systemUnderTest.ViewModel.GetPropertyValue<string>( TreasureSetPM.NAME_PROPERTY ) );
        }
    }
}
