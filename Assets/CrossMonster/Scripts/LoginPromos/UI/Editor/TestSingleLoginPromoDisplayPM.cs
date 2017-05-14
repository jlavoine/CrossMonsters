using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestSingleLoginPromoDisplayPM : ZenjectUnitTestFixture {
        private IStringTableManager MockStringTable;
        private ILoginPromotionData MockData;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockData = Substitute.For<ILoginPromotionData>();
        }

        [Test]
        public void WhenCreating_TitleSetAsExpected() {
            MockData.GetNameKey().Returns( "TestId" );
            MockStringTable.Get( "TestId" ).Returns( "TestTitle" );

            SingleLoginPromoDisplayPM systemUnderTest = new SingleLoginPromoDisplayPM( MockStringTable, MockData );

            Assert.AreEqual( "TestTitle", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleLoginPromoDisplayPM.TITLE_PROPERTY ) );
        }
    }
}
