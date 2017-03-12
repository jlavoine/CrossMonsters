﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestDungeonLoader : ZenjectUnitTestFixture {

        [Inject]
        ILoadingScreenPM MockLoadingPM;

        [Inject]
        IBackendManager MockBackendManager;

        [Inject]
        ICurrentDungeonGameManager MockCurrentDungeon;

        [Inject]
        ISceneManager MockSceneManager;

        [Inject]
        DungeonLoader systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ILoadingScreenPM>().FromInstance( Substitute.For<ILoadingScreenPM>() );
            Container.Bind<IBackendManager>().FromInstance( Substitute.For<IBackendManager>() );
            Container.Bind<ICurrentDungeonGameManager>().FromInstance( Substitute.For<ICurrentDungeonGameManager>() );
            Container.Bind<ISceneManager>().FromInstance( Substitute.For<ISceneManager>() );
            Container.Bind<DungeonLoader>().AsSingle();       
            Container.Inject( this );
        }

        [Test]
        public void OnClick_LoadingScreenShowIsCalled() {
            systemUnderTest.OnClick();

            MockLoadingPM.Received().Show();
        }

        [Test]
        public void OnClick_BackendCloudCallMade_ToGetGameSession() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            MockBackendManager.GetBackend<IBasicBackend>().Returns( mockBackend );
            systemUnderTest.OnClick();

            mockBackend.Received().MakeCloudCall( "getDungeonGameSession", Arg.Any<Dictionary<string, string>>(), Arg.Any<Callback<Dictionary<string, string>>>() );
        }

        [Test]
        public void WhenReceivingDungeonData_SetsDataOnCurrentDungeonManager() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            systemUnderTest.OnDungeonGameSessionResponse( mockData );

            MockCurrentDungeon.Received().SetData( mockData );
        }

        [Test]
        public void WhenReceivingDungeonData_GameSceneIsLoaded() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            systemUnderTest.OnDungeonGameSessionResponse( mockData );

            MockSceneManager.Received().LoadScene( "Playground" );
        }
    }
}
