using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTreasure : ZenjectUnitTestFixture {

        private ITreasureData MockData;
        private ITreasureDataManager MockManager;

        [SetUp]
        public void CommonInstall() {
            MockData = Substitute.For<ITreasureData>();
            MockManager = Substitute.For<ITreasureDataManager>();
        }

        private Treasure CreateSystem() {
            Treasure systemUnderTest = new Treasure( MockManager, MockData );
            return systemUnderTest;
        }

        [Test]
        public void GetValue_ReturnsManagerRarityValue() {
            MockManager.GetValueForRarity( "SomeRarity" ).Returns( 10 );
            MockData.GetRarity().Returns( "SomeRarity" );

            Treasure systemUnderTest = CreateSystem();

            Assert.AreEqual( 10, systemUnderTest.GetValue() );
        }
    }
}
