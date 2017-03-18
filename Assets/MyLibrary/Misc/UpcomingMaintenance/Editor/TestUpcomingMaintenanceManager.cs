using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestUpcomingMaintenanceManager : ZenjectUnitTestFixture {

        [Inject]
        IMessageService MockMessenger;

        [Inject]
        UpcomingMaintenanceManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<UpcomingMaintenanceManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_UpcomingMaintenanceData_IsDownloaded() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( UpcomingMaintenanceManager.UPCOMING_MAINTENANCE_TITLE_KEY, Arg.Any<Callback<string>>() );
        }

        [Test]
        public void WhenDataHasUpcomingMaintenance_IsAnyUpcomingMaintenance_IsTrue() {
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.IsAnyUpcomingMaintenance().Returns( true );
            systemUnderTest.Data = mockData;

            Assert.IsTrue( systemUnderTest.IsAnyUpcomingMaintenance() );
        }

        [Test]
        public void WhenDataHasNoUpcomingMaintenance_IsAnyUpcomingMaintenance_IsFalse() {
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.IsAnyUpcomingMaintenance().Returns( false );
            systemUnderTest.Data = mockData;

            Assert.IsFalse( systemUnderTest.IsAnyUpcomingMaintenance() );
        }

        static object[] WithinWaringTests = {
            new object[] { 100000, 0, 1, false },
            new object[] { 100000, 100000, 1, true },
            new object[] { 160000, 100000, 1, true },
            new object[] { 100000, 100001, 1, true }
        };

        [Test, TestCaseSource( "WithinWaringTests" )]
        public void IsWithinWarningTime_ReturnsAsExpected( double i_maintenanceTimeFromEpoch, double i_curTimeFromEpoch, int i_warningTimeInMinutes, bool i_expected ) {
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.GetWarningTimeInMinutes().Returns( i_warningTimeInMinutes );
            mockData.GetStartSecondsFromEpoch().Returns( i_maintenanceTimeFromEpoch / 1000 );
            systemUnderTest.Data = mockData;

            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();            
            DateTime curTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            curTime = curTime.AddMilliseconds( i_curTimeFromEpoch );
            mockBackend.GetDateTime().Returns( curTime );
            systemUnderTest.Init( mockBackend );

            Assert.AreEqual( i_expected, systemUnderTest.IsWithinWarningTime() );
        }

        static object[] DuringMaintenanceTests = {
            new object[] { 100000, 0, false },
            new object[] { 100000, 100000, true },
            new object[] { 160000, 100000, false },
            new object[] { 100000, 100001, true }
        };

        [Test, TestCaseSource( "DuringMaintenanceTests" )]
        public void IsDuringMaintenance_ReturnsAsExpected( double i_maintenanceTimeFromEpoch, double i_curTimeFromEpoch, bool i_expected ) {
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.GetStartSecondsFromEpoch().Returns( i_maintenanceTimeFromEpoch / 1000 );
            systemUnderTest.Data = mockData;

            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            DateTime curTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            curTime = curTime.AddMilliseconds( i_curTimeFromEpoch );
            mockBackend.GetDateTime().Returns( curTime );
            systemUnderTest.Init( mockBackend );

            Assert.AreEqual( i_expected, systemUnderTest.IsDuringMaintenance() );
        }

        [Test]
        public void WhenTriggeringUpcomingMaintenanceView_TriggerMessageIsSent() {
            // this is a mess...teaches me the need to even further break stuff out I think
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.GetStartSecondsFromEpoch().Returns( 1000 );
            systemUnderTest.Data = mockData;

            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            mockBackend.GetDateTime().Returns( DateTime.Now );
            systemUnderTest.Init( mockBackend );
            systemUnderTest.TriggerUpcomingMaintenanceView();

            MockMessenger.Received().Send<bool>( UpcomingMaintenanceManager.TRIGGER_MAINTENANCE_POPUP, Arg.Any<bool>() );
        }

        [Test]
        public void WhenTriggeringUpcomingMaintenanceView_HasUserBeenWarned_IsTrue() {
            // this is a mess...teaches me the need to even further break stuff out I think
            IUpcomingMaintenanceData mockData = Substitute.For<IUpcomingMaintenanceData>();
            mockData.GetStartSecondsFromEpoch().Returns( 1000 );
            systemUnderTest.Data = mockData;

            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            mockBackend.GetDateTime().Returns( DateTime.Now );
            systemUnderTest.Init( mockBackend );
            systemUnderTest.HasUserBeenWarned = false;
            systemUnderTest.TriggerUpcomingMaintenanceView();

            Assert.IsTrue( systemUnderTest.HasUserBeenWarned );
        }
    }
}
