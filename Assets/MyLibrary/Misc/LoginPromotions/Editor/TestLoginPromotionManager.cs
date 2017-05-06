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
    public class TestLoginPromotionManager : ZenjectUnitTestFixture {
        [Inject]
        LoginPromotionManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<LoginPromotionManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_GetTitleDataCall_MadeToBackend() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( LoginPromotionManager.PROMOTIONS_TITLE_KEY, Arg.Any<Callback<string>>() );
        }

       [Test]
       public void PromotionAdded_ToActivePromos_WhenActive() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            ILoginPromotionData mockPromo = Substitute.For<ILoginPromotionData>();
            mockPromo.IsActive( Arg.Any<DateTime>() ).Returns( true );
            systemUnderTest.ActivePromotionData = new List<ILoginPromotionData>();

            systemUnderTest.AddToActivePromosIfActive( mockPromo );

            Assert.AreEqual( 1, systemUnderTest.ActivePromotionData.Count );
        }

        [Test]
        public void PromotionNotAdded_ToActivePromos_WhenNotActive() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            ILoginPromotionData mockPromo = Substitute.For<ILoginPromotionData>();
            mockPromo.IsActive( Arg.Any<DateTime>() ).Returns( false );
            systemUnderTest.ActivePromotionData = new List<ILoginPromotionData>();

            systemUnderTest.AddToActivePromosIfActive( mockPromo );

            Assert.AreEqual( 0, systemUnderTest.ActivePromotionData.Count );
        }
    }
}