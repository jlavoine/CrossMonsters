using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;
using System;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestShowLoginPromosStep : ZenjectUnitTestFixture {
        private IMessageService MockMessenger;
        private ILoginPromotionManager MockPromoManager;
        private ILoginPromoDisplaysPM MockAllPromosPM;
        private ISceneStartFlowManager MockFlowManager;
        private ILoginPromoPopupHelper MockHelper;
        private IBackendManager MockBackend;

        [SetUp]
        public void CommonInstall() {
            MockBackend = Substitute.For<IBackendManager>();
            MockMessenger = Substitute.For<IMessageService>();
            MockPromoManager = Substitute.For<ILoginPromotionManager>();
            MockAllPromosPM = Substitute.For<ILoginPromoDisplaysPM>();
            MockFlowManager = Substitute.For<ISceneStartFlowManager>();
            MockHelper = Substitute.For<ILoginPromoPopupHelper>();

            SetActiveProgressOnMockManagerWithIds( new List<string>() );
        }

        [Test]
        public void WhenStarting_ExpectedMessagesSubscribed() {
            ShowLoginPromosStep systemUnderTest = CreateSystem();

            systemUnderTest.Start();

            MockMessenger.Received().AddListener( SingleLoginPromoDisplayPM.PROMO_DISMISSED_EVENT, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDone_ExpectedMessagesUnsubscribed() {
            ShowLoginPromosStep systemUnderTest = CreateSystem();

            systemUnderTest.Done();

            CheckStepIsDone();            
        }

        [Test]
        public void WhenCreated_ActivePromos_MatchesManagerActivePromos() {
            List<ISingleLoginPromoProgressSaveData> mockActivePromos = new List<ISingleLoginPromoProgressSaveData>();
            MockPromoManager.GetActivePromoSaveData().Returns( mockActivePromos );

            ShowLoginPromosStep systemUnderTest = CreateSystem();

            Assert.AreEqual( mockActivePromos, systemUnderTest.ActivePromoSaveData );
        }

        [Test]
        public void WhenStarted_IfNoActivePromos_StepIsDone() {           
            ShowLoginPromosStep systemUnderTest = CreateSystem();

            systemUnderTest.Start();

            CheckStepIsDone();
        }

        [Test]
        public void WhenStarted_IfActivePromos_AndPromoShouldShow_FirstPromoIsShown() {
            MockHelper.ShouldShowPromoAsPopup( Arg.Any<ISingleLoginPromoProgressSaveData>(), Arg.Any<ILoginPromotionData>() ).Returns( true );
            SetActiveProgressOnMockManagerWithIds( new List<string>() { "Test_1" } );

            ShowLoginPromosStep systemUnderTest = CreateSystem();
            systemUnderTest.Start();

            MockAllPromosPM.Received().DisplayPromoAndHideOthers( "Test_1" );
            MockHelper.Received().AwardPromoOnClient( Arg.Any<ISingleLoginPromoProgressSaveData>(), Arg.Any<ILoginPromotionData>() );
            MockHelper.Received().AwardPromoOnServer( Arg.Any<ILoginPromotionData>() );
        }

        [Test]
        public void WhenStarted_IfActivePromos_AndPromoShouldNotShow_PromoIsNotShown() {
            MockHelper.ShouldShowPromoAsPopup( Arg.Any<ISingleLoginPromoProgressSaveData>(), Arg.Any<ILoginPromotionData>() ).Returns( false );
            SetActiveProgressOnMockManagerWithIds( new List<string>() { "Test_1" } );

            ShowLoginPromosStep systemUnderTest = CreateSystem();
            systemUnderTest.Start();

            MockAllPromosPM.DidNotReceive().DisplayPromoAndHideOthers( "Test_1" );
        }

        [Test]
        public void WhenPromoIsDismissed_IfNoOtherPromos_StepIsDone() {
            SetActiveProgressOnMockManagerWithIds( new List<string>() { "Test_1" } );

            ShowLoginPromosStep systemUnderTest = CreateSystem();
            systemUnderTest.Start();
            systemUnderTest.ProcessNextPromotion();

            CheckStepIsDone();
        }

        [Test]
        public void WhenPromoIsDismissed_IfOtherPromos_NextPromoIsShown() {
            MockHelper.ShouldShowPromoAsPopup( Arg.Any<ISingleLoginPromoProgressSaveData>(), Arg.Any<ILoginPromotionData>() ).Returns( true );
            SetActiveProgressOnMockManagerWithIds( new List<string>() { "Test_1", "Test_2" } );

            ShowLoginPromosStep systemUnderTest = CreateSystem();
            systemUnderTest.Start();
            systemUnderTest.ProcessNextPromotion();

            MockAllPromosPM.Received().DisplayPromoAndHideOthers( "Test_2" );
        }

        private void SetActiveProgressOnMockManagerWithIds( List<string> i_ids ) {
            List<ISingleLoginPromoProgressSaveData> mockActivePromos = new List<ISingleLoginPromoProgressSaveData>();

            foreach ( string id in i_ids ) {
                ISingleLoginPromoProgressSaveData mockProgress = Substitute.For<ISingleLoginPromoProgressSaveData>();
                mockProgress.GetId().Returns( id );
                mockActivePromos.Add( mockProgress );                
            }

            MockPromoManager.GetActivePromoSaveData().Returns( mockActivePromos );
        }

        private void CheckStepIsDone() {
            MockMessenger.Received().RemoveListener( SingleLoginPromoDisplayPM.PROMO_DISMISSED_EVENT, Arg.Any<Callback>() );
        }

        private ShowLoginPromosStep CreateSystem() {
            ShowLoginPromosStep systemUnderTest = new ShowLoginPromosStep( MockBackend, MockHelper, MockAllPromosPM, MockPromoManager, MockMessenger, MockFlowManager );
            return systemUnderTest;
        }
    }
}
