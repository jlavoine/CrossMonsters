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
            LoginPromoDisplaysPM systemUnderTest = new LoginPromoDisplaysPM( MockSpawner, MockManager );

            Assert.AreEqual( 2, systemUnderTest.DisplayPMs.Count );
        }
    }
}
