using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestUpcomingMaintenancePM : ZenjectUnitTestFixture {

        [Inject]
        IMessageService MockMessenger;

        [Inject]
        IStringTableManager MockStringTable;

        [Inject]
        IUpcomingMaintenanceManager MockMaintenanceManager;

        [Inject]
        UpcomingMaintenancePM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IStringTableManager>().FromInstance( Substitute.For<IStringTableManager>() );
            Container.Bind<IUpcomingMaintenanceManager>().FromInstance( Substitute.For<IUpcomingMaintenanceManager>() );
            Container.Bind<UpcomingMaintenancePM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenCreating_IsVisible_IsFalse() {
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_CanDismiss_IsFalse() {
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.CAN_DISMISS_PROPERTY ) );
        }

        [Test]
        public void WhenDismissed_DismissEvent_IsSent() {
            systemUnderTest.Dismiss();

            MockMessenger.Received().Send( UpcomingMaintenancePM.DISMISSED_EVENT );
        }

        [Test]
        public void WhenDismissed_IsVisible_IsFalse() {
            systemUnderTest.ViewModel.SetProperty( UpcomingMaintenancePM.VISIBLE_PROPERTY, true );

            systemUnderTest.Dismiss();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenTriggered_IsVisible_IsTrue() {
            systemUnderTest.OnTrigger( true );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenTriggered_IfCanDismiss_CanDismiss_IsTrue() {
            systemUnderTest.OnTrigger( true );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.CAN_DISMISS_PROPERTY ) );
        }

        [Test]
        public void WhenTriggered_IfCannotDismiss_CanDismiss_IsFalse() {
            systemUnderTest.OnTrigger( false );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( UpcomingMaintenancePM.CAN_DISMISS_PROPERTY ) );
        }
    }
}