using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestMainMenuFlow : ZenjectUnitTestFixture {
        [Inject]
        MainMenuFlow systemUnderTest;

        [Inject]
        IUpcomingMaintenanceFlowStepSpawner MockMaintenanceStepSpawner;

        [Inject]
        IShowNewsStepSpawner MockNewsStepSpawner;

        [Inject]
        IShowLoginPromosStepSpawner MockLoginPromoStepSpawner;

        [Inject]
        IShowGauntletStepSpawner MockShowGauntletStepSpawner;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IUpcomingMaintenanceFlowStepSpawner>().FromInstance( Substitute.For<IUpcomingMaintenanceFlowStepSpawner>() );
            Container.Bind<IShowNewsStepSpawner>().FromInstance( Substitute.For<IShowNewsStepSpawner>() );
            Container.Bind<IShowLoginPromosStepSpawner>().FromInstance( Substitute.For<IShowLoginPromosStepSpawner>() );
            Container.Bind<IShowGauntletStepSpawner>().FromInstance( Substitute.For<IShowGauntletStepSpawner>() );
            Container.Bind<MainMenuFlow>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenStarting_ExpectedStepsAreCreated() {            
            systemUnderTest.Start();

            MockMaintenanceStepSpawner.Received().Create( systemUnderTest );
            MockNewsStepSpawner.Received().Create( systemUnderTest );
            MockLoginPromoStepSpawner.Received().Create( systemUnderTest );
            MockShowGauntletStepSpawner.Received().Create( systemUnderTest );
        }
    }
}