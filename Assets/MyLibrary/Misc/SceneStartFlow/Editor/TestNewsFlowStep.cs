using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System;

#pragma warning disable 0219

namespace MyLibrary {
    [TestFixture]
    public class TestNewsFlowStep : ZenjectUnitTestFixture {

        [Inject]
        IMessageService MockMessenger;

        [Inject]
        INewsManager MockNewsManager;

        [Inject]
        IAllNewsPM MockNewsPM;

        [Inject]
        ISceneStartFlowManager MockSceneFlowManager;

        [Inject]
        ShowNewsFlowStep systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ISceneStartFlowManager>().FromInstance( Substitute.For<ISceneStartFlowManager>() );
            Container.Bind<IMessageService>().FromInstance( Substitute.For<IMessageService>() );
            Container.Bind<INewsManager>().FromInstance( Substitute.For<INewsManager>() );
            Container.Bind<IAllNewsPM>().FromInstance( Substitute.For<IAllNewsPM>() );
            Container.Bind<ShowNewsFlowStep>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenStarting_ExpectedMessagesSubscribed() {
            systemUnderTest.Start();

            MockMessenger.Received().AddListener( AllNewsPM.NEWS_DIMISSED_EVENT, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDone_ExpectedMessagesUnsubscribed() {
            systemUnderTest.Done();

            MockMessenger.Received().RemoveListener( AllNewsPM.NEWS_DIMISSED_EVENT, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenStarting_IfShouldShowNews_NewsIsShown_And_LastSeenNewsTimeUpdated() {
            MockNewsManager.ShouldShowNews().Returns( true );
            systemUnderTest.Start();

            MockNewsPM.Received().Show();
            MockNewsManager.Received().UpdateLastSeenNewsTime();
        }

        [Test]
        public void WhenStarting_IfShouldNotShowNews_NothingHappens() {
            MockNewsManager.ShouldShowNews().Returns( false );
            systemUnderTest.Start();

            MockNewsPM.DidNotReceive().Show();
            MockNewsManager.DidNotReceive().UpdateLastSeenNewsTime();
        }
    }
}
