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

            mockBackend.Received().GetPublicPlayerData( NewsManager.LAST_SEEN_NEWS_KEY, Arg.Any<Callback<string>>() );
        }

        static object[] ShouldShowNewsTests = {
            new object[] { new DateTime(1000), new DateTime(0), true },
            new object[] { new DateTime(1000), new DateTime(1000), false },
            new object[] { new DateTime(1000), new DateTime(2000), false }
        };

        [Test, TestCaseSource( "ShouldShowNewsTests" )]
        public void ShouldShowNews_ReturnsAsExpected( DateTime i_latestNewsTime, DateTime i_lastSeenTime, bool i_expected ) {
            IBasicNewsData mockNews = Substitute.For<IBasicNewsData>();
            mockNews.GetTimestamp().Returns( i_latestNewsTime );
            systemUnderTest.NewsList = new List<IBasicNewsData>() { mockNews };
            systemUnderTest.LastSeenNewsTime = i_lastSeenTime;

            bool shouldShow = systemUnderTest.ShouldShowNews();

            Assert.AreEqual( i_expected, shouldShow );
        }

        [Test]
        public void WhenNoNews_ShouldShowNews_ReturnsFalse() {
            bool shouldShow = systemUnderTest.ShouldShowNews();

            Assert.IsFalse( shouldShow );
        }
    }
}