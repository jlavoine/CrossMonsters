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
        private ILoginPromoDisplaysPM MockDisplayPM;

        [SetUp]
        public void CommonInstall() {
            MockStringTable = Substitute.For<IStringTableManager>();
            MockData = Substitute.For<ILoginPromotionData>();
            MockDisplayPM = Substitute.For<ILoginPromoDisplaysPM>();
        }

        [Test]
        public void WhenCreated_NameProperty_SetAsExpected() {
            MockData.GetNameKey().Returns( "TestKey" );
            MockStringTable.Get( "TestKey" ).Returns( "TestName" );

            ActiveLoginPromoButtonPM systemUnderTest = CreateSystem();

            Assert.AreEqual( "TestName", systemUnderTest.ViewModel.GetPropertyValue<string>( ActiveLoginPromoButtonPM.NAME_PROPERTY ) );
        }

        [Test]
        public void WhenOpenDisplayClicked_MainPM_IsNotified() {
            MockData.GetId().Returns( "TestId" );
            ActiveLoginPromoButtonPM systemUnderTest = CreateSystem();

            systemUnderTest.OpenDisplayClicked();

            MockDisplayPM.Received().DisplayPromoAndHideOthers( "TestId" );
        }

        private ActiveLoginPromoButtonPM CreateSystem() {
            ActiveLoginPromoButtonPM systemUnderTest = new ActiveLoginPromoButtonPM( MockDisplayPM, MockStringTable, MockData );

            return systemUnderTest;
        }
    }
}
