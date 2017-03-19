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
        IUpcomingMaintenanceFlowStepSpawner MockMaintenanceStepSpawner;

        [Inject]
        IShowNewsStepSpawner MockNewsStepSpawner;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IUpcomingMaintenanceFlowStepSpawner>().FromInstance( Substitute.For<IUpcomingMaintenanceFlowStepSpawner>() );
            Container.Bind<IShowNewsStepSpawner>().FromInstance( Substitute.For<IShowNewsStepSpawner>() );
            Container.Bind<MainMenuFlow>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenStarting_ExpectedStepsAreCreated() {            
            systemUnderTest.Start();

            MockMaintenanceStepSpawner.Received().Create( systemUnderTest );
            MockNewsStepSpawner.Received().Create( systemUnderTest );
        }
    }
}