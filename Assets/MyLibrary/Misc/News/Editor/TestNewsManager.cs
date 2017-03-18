using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestNewsManager : ZenjectUnitTestFixture {
        [Inject]
        NewsManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            //Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<NewsManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_GetNewsCall_MadeToBackend() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetNews( Arg.Any<Callback<List<IBasicNewsData>>>() );
        }

        [Test]
        public void WhenIniting_GetLastSeenNewsTime_FromPlayerData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetReadOnlyPlayerData( NewsManager.LAST_SEEN_NEWS_KEY, Arg.Any<Callback<string>>() );
        }
    }
}