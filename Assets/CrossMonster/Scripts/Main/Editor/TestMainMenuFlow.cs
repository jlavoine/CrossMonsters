using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestMainMenuFlow : ZenjectUnitTestFixture {
        [Inject]
        MainMenuFlow systemUnderTest;

        [Inject]
        IUpcomingMaintenanceManager MockMaintenanceManager;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IUpcomingMaintenanceManager>().FromInstance( Substitute.For<IUpcomingMaintenanceManager>() );
            Container.Bind<MainMenuFlow>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenStarting_IfUpcomingMaintenance_ViewIsTriggered() {
            MockMaintenanceManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ).Returns( true );
            systemUnderTest.Start();

            MockMaintenanceManager.Received().TriggerUpcomingMaintenanceView();
        }

        [Test]
        public void WhenStarting_IfNoUpcomingMaintenance_ViewIsNotTriggered() {
            MockMaintenanceManager.ShouldTriggerUpcomingMaintenanceView( MaintenanceConcernLevels.Close ).Returns( false );
            systemUnderTest.Start();

            MockMaintenanceManager.DidNotReceive().TriggerUpcomingMaintenanceView();
        }
    }
}