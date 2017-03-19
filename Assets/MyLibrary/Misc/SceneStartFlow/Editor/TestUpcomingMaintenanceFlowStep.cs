using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestUpcomingMaintenanceFlowStep : ZenjectUnitTestFixture {

        [Inject]
        IMessageService MockMessenger;

        [Inject]
        IUpcomingMaintenanceManager MockManager;

        [Inject]
        ISceneStartFlowManager MockSceneFlowManager;

        [Inject]
        UpcomingMaintenanceFlowStep systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ISceneStartFlowManager>().FromInstance( Substitute.For<ISceneStartFlowManager>() );
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<IUpcomingMaintenanceManager>().FromInstance( Substitute.For<IUpcomingMaintenanceManager>() );
            Container.Bind<UpcomingMaintenanceFlowStep>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenStarting_ExpectedMessagesSubscribed() {
            systemUnderTest.Start();

            MockMessenger.Received().AddListener( UpcomingMaintenancePM.DISMISSED_EVENT, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDone_ExpectedMessagesUnsubscribed() {
            systemUnderTest.Done();

            MockMessenger.Received().RemoveListener( UpcomingMaintenancePM.DISMISSED_EVENT, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenStarting_IfShouldTriggerMaintenance_IsTriggered() {
            MockManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ).Returns( true );
            systemUnderTest.Start();

            MockManager.Received().TriggerUpcomingMaintenanceView();
        }

        [Test]
        public void WhenStarting_IfShouldNotTriggerMaintenance_NothingHappens() {
            MockManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ).Returns( false );
            systemUnderTest.Start();

            MockManager.DidNotReceive().TriggerUpcomingMaintenanceView();
        }
    }
}
