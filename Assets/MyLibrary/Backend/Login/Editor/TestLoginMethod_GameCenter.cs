﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    public class TestLoginMethod_GameCenter : ZenjectUnitTestFixture {
        // Can't test this right now - I should/need to abstract out the code that gets the Game Center id for this to be testable
        /*[Inject]
        IBackendManager MockBackendManager;

        [Inject]
        LoginMethod_DeviceId systemUnderTest;

        private IBasicBackend MockBackend;

        [SetUp]
        public void CommonInstall() {
            MockBackend = Substitute.For<IBasicBackend>();
            Container.Bind<IBackendManager>().FromInstance( Substitute.For<IBackendManager>() );
            Container.Bind<LoginMethod_DeviceId>().AsSingle();
            Container.Inject( this );

            MockBackendManager.GetBackend<IBasicBackend>().Returns( MockBackend );
        }

        [Test]
        public void WhenAuthing_BackendAuthenticationIsCalled() {
            systemUnderTest.Authenticate();

            MockBackend.Received().Authenticate( Arg.Any<string>() );
        }*/
    }
}
