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
    public class TestActiveLoginPromoPM : ZenjectUnitTestFixture {

        private IActiveLoginPromoButtonPM_Spawner MockSpawner;
        private ILoginPromotionManager MockManager;

        [SetUp]
        public void CommonInstall() {
            MockSpawner = Substitute.For<IActiveLoginPromoButtonPM_Spawner>();
            MockManager = Substitute.For<ILoginPromotionManager>();
        }

        [Test]
        public void WhenCreated_CreatedButtonPMs_MatchActivePromos() {
            MockManager.ActivePromotionData.Returns( new List<ILoginPromotionData>() { Substitute.For<ILoginPromotionData>(), Substitute.For<ILoginPromotionData>() } );
            ActiveLoginPromoPM systemUnderTest = new ActiveLoginPromoPM( MockSpawner, MockManager );

            Assert.AreEqual( 2, systemUnderTest.ButtonPMs.Count );
        }
    }
}
