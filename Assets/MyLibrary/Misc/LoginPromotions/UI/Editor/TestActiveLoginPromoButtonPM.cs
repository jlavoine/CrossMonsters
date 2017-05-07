using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    [TestFixture]
    public class TestActiveLoginPromoButtonPM : ZenjectUnitTestFixture {

        private IStringTableManager MockStringTable;
        private ILoginPromotionData MockData;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockData = Substitute.For<ILoginPromotionData>();
        }

        [Test]
        public void WhenCreated_NameProperty_SetAsExpected() {
            MockData.GetNameKey().Returns( "TestKey" );
            MockStringTable.Get( "TestKey" ).Returns( "TestName" );

            ActiveLoginPromoButtonPM systemUnderTest = new ActiveLoginPromoButtonPM( MockStringTable, MockData );

            Assert.AreEqual( "TestName", systemUnderTest.ViewModel.GetPropertyValue<string>( ActiveLoginPromoButtonPM.NAME_PROPERTY ) );
        }
    }
}
