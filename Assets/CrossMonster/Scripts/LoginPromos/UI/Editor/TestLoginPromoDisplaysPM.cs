using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestLoginPromoDisplaysPM : ZenjectUnitTestFixture {

        private ILoginPromotionManager MockManager;
        private ISingleLoginPromoPM_Spawner MockSpawner;

        [SetUp]
        public void CommonInstall() {
            MockManager = Substitute.For<ILoginPromotionManager>();
            MockSpawner = Substitute.For<ISingleLoginPromoPM_Spawner>();
        }

        [Test]
        public void WhenCreating_ForEachActivePromo_DisplayPM_IsSpawned() {
            MockManager.ActivePromotionData.Returns( new List<ILoginPromotionData>() { Substitute.For<ILoginPromotionData>(), Substitute.For<ILoginPromotionData>() } );
            LoginPromoDisplaysPM systemUnderTest = CreateSystem();

            Assert.AreEqual( 2, systemUnderTest.DisplayPMs.Count );
        }

        [Test]
        public void WhenDisplayingPromo_EachPromoPM_IsNotified() {
            LoginPromoDisplaysPM systemUnderTest = CreateSystem();
            systemUnderTest.DisplayPMs = CreateMockDisplayPMs( 5 );

            systemUnderTest.DisplayPromoAndHideOthers( "Test" );

            foreach ( ISingleLoginPromoDisplayPM pm in systemUnderTest.DisplayPMs ) {
                pm.Received().UpdateVisibilityBasedOnCurrentlyDisplayedPromo( "Test" );
            }
        }

        private List<ISingleLoginPromoDisplayPM> CreateMockDisplayPMs( int i_count ) {
            List<ISingleLoginPromoDisplayPM> list = new List<ISingleLoginPromoDisplayPM>();
            for ( int i = 0; i < i_count; ++i ) {
                list.Add( Substitute.For<ISingleLoginPromoDisplayPM>() );
            }

            return list;
        }

        private LoginPromoDisplaysPM CreateSystem() {
            LoginPromoDisplaysPM systemUnderTest = new LoginPromoDisplaysPM( MockSpawner, MockManager );
            return systemUnderTest;
        }
    }
}
