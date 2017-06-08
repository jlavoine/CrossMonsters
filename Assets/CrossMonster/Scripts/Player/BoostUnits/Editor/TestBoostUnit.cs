using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestBoostUnit : ZenjectUnitTestFixture {
        private IMyItemInstance MockItemInstance;
        private IBoostUnitCustomData MockCustomData;

        [SetUp]
        public void CommonInstall() {
            MockItemInstance = Substitute.For<IMyItemInstance>();
            MockCustomData = Substitute.For<IBoostUnitCustomData>();
        }

        private BoostUnit CreateSystem() {
            BoostUnit systemUnderTest = new BoostUnit( MockItemInstance, MockCustomData );
            return systemUnderTest;
        }

        [Test]
        public void HasEffect_ReturnsTrue_IfCustomDataHasEffect() {
            MockCustomData.HasEffect( "Test" ).Returns( true );

            BoostUnit systemUnderTest = CreateSystem();

            Assert.IsTrue( systemUnderTest.HasEffect( "Test" ) );
        }

        [Test]
        public void HasEffect_ReturnsFalse_IfCustomDataDoesNotHaveEffect() {
            MockCustomData.HasEffect( "Test" ).Returns( false );

            BoostUnit systemUnderTest = CreateSystem();

            Assert.IsFalse( systemUnderTest.HasEffect( "Test" ) );
        }

        [Test]
        public void GetEffect_ReturnsZero_IfCustomDataDoesNotHaveEffect() {
            MockCustomData.HasEffect( "Test" ).Returns( false );

            BoostUnit systemUnderTest = CreateSystem();

            Assert.AreEqual( 0, systemUnderTest.GetEffect( "Test" ) );
        }

        [Test]
        public void GetEffect_ReturnsItemCountMultipliedByDataEffect() {
            MockItemInstance.GetCount().Returns( 3 );
            MockCustomData.GetEffect( "Test" ).Returns( 5 );
            MockCustomData.HasEffect( "Test" ).Returns( true );

            BoostUnit systemUnderTest = CreateSystem();

            Assert.AreEqual( 15, systemUnderTest.GetEffect( "Test" ) );
        }
    }
}