using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestTreasureDataManager : ZenjectUnitTestFixture {

        [Inject]
        TreasureDataManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {            
            Container.Bind<TreasureDataManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenIniting_CallsToBackendForData() {
            IBasicBackend mockBackend = Substitute.For<IBasicBackend>();
            systemUnderTest.Init( mockBackend );

            mockBackend.Received().GetTitleData( TreasureDataManager.TREASURE_DATA_TITLE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetTitleData( TreasureDataManager.TREASURE_SETS_TITLE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetTitleData( TreasureDataManager.TREASURE_VALUE_KEY, Arg.Any<Callback<string>>() );
            mockBackend.Received().GetReadOnlyPlayerData( TreasureDataManager.TREASURE_PROGRESS_KEY, Arg.Any<Callback<string>>() );
        }

        [Test]
        public void IfRarityExists_GetValue_ReturnsValue() {
            systemUnderTest.TreasureValues = new Dictionary<string, int>() { { "Rarity_A", 10 }, { "Rarity_B", 20 } };

            int valueA = systemUnderTest.GetValueForRarity( "Rarity_A" );
            int valueB = systemUnderTest.GetValueForRarity( "Rarity_B" );

            Assert.AreEqual( valueA, 10 );
            Assert.AreEqual( valueB, 20 );
        }

        [Test]
        public void IfRarityDoesNotExist_GetValue_ReturnsZero() {
            systemUnderTest.TreasureValues = new Dictionary<string, int>() { { "Rarity_A", 10 }, { "Rarity_B", 20 } };

            int valueC = systemUnderTest.GetValueForRarity( "Rarity_C" );

            Assert.AreEqual( valueC, 0 );
        }
    }
}
