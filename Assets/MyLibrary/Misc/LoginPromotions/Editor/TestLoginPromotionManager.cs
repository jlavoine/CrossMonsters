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
        public void WhenIniting_GetPromoSaveDataCall_MadeToBackend() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetReadOnlyPlayerData( LoginPromotionManager.PROMO_PROGRESS_KEY, Arg.Any<Callback<string>>() );
        }

        [Test]
        public void GettingListOfActivePromoSaveData_ReturnsAsExpected() {
            // this test kind of sucks. I think I've learned my lesson. Nothing should have a List<T>, it should have a structure that 
            // holds a List<T> I think, so I can easier mock its functionality
            Dictionary<string, ILoginPromotionData> mockActiveData = new Dictionary<string, ILoginPromotionData>();
            ILoginPromotionData mockData_1 = Substitute.For<ILoginPromotionData>();
            mockData_1.GetId().Returns( "A" );
            ILoginPromotionData mockData_2 = Substitute.For<ILoginPromotionData>();
            mockData_2.GetId().Returns( "C" );
            mockActiveData.Add( "A", mockData_1 );
            mockActiveData.Add( "C", mockData_2 );
            systemUnderTest.ActivePromotionData = mockActiveData;

            Dictionary<string, ISingleLoginPromoProgressSaveData> mockSaveProgress = new Dictionary<string, ISingleLoginPromoProgressSaveData>();
            ISingleLoginPromoProgressSaveData mockSave_1 = Substitute.For<ISingleLoginPromoProgressSaveData>();
            ISingleLoginPromoProgressSaveData mockSave_2 = Substitute.For<ISingleLoginPromoProgressSaveData>();
            ISingleLoginPromoProgressSaveData mockSave_3 = Substitute.For<ISingleLoginPromoProgressSaveData>();
            mockSaveProgress.Add( "A", mockSave_1 );
            mockSaveProgress.Add( "C", mockSave_2 );
            mockSaveProgress.Add( "B", mockSave_3 );
            systemUnderTest.PromoProgress = mockSaveProgress;

            List<ISingleLoginPromoProgressSaveData> result = systemUnderTest.GetActivePromoSaveData();

            Assert.Contains( mockSave_1, result );
            Assert.Contains( mockSave_2, result );
            Assert.AreEqual( 2, result.Count );
        }

        [Test]
        public void PromotionAdded_ToActivePromos_WhenActive() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            ILoginPromotionData mockPromo = Substitute.For<ILoginPromotionData>();
            mockPromo.IsActive( Arg.Any<DateTime>() ).Returns( true );
            systemUnderTest.ActivePromotionData = new Dictionary<string, ILoginPromotionData>();

            systemUnderTest.AddToActivePromosIfActive( mockPromo );

            Assert.AreEqual( 1, systemUnderTest.ActivePromotionData.Count );
        }

        [Test]
        public void PromotionNotAdded_ToActivePromos_WhenNotActive() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            ILoginPromotionData mockPromo = Substitute.For<ILoginPromotionData>();
            mockPromo.IsActive( Arg.Any<DateTime>() ).Returns( false );
            systemUnderTest.ActivePromotionData = new Dictionary<string, ILoginPromotionData>();

            systemUnderTest.AddToActivePromosIfActive( mockPromo );

            Assert.AreEqual( 0, systemUnderTest.ActivePromotionData.Count );
        }

        [Test]
        public void GetDataForPromo_ReturnsNullIfNoData() {
            systemUnderTest.ActivePromotionData = new Dictionary<string, ILoginPromotionData>();

            ILoginPromotionData data = systemUnderTest.GetDataForPromo( "SomePromo" );

            Assert.IsNull( data );
        }

        [Test]
        public void GetDataForPromo_ReturnsData_WhenItExists() {
            systemUnderTest.ActivePromotionData = new Dictionary<string, ILoginPromotionData>() { { "SomePromo", Substitute.For<ILoginPromotionData>() } };

            ILoginPromotionData data = systemUnderTest.GetDataForPromo( "SomePromo" );

            Assert.IsNotNull( data );
        }
    }
}