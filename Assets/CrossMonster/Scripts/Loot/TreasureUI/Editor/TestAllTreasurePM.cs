using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestAllTreasurePM : ZenjectUnitTestFixture {

        [Inject]
        ITreasureDataManager TreasureDataManager;

        [Inject]
        ITreasureSetPM_Spawner TreasurePM_Spawner;

        [Inject]
        AllTreasurePM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<ITreasureDataManager>().FromInstance( Substitute.For<ITreasureDataManager>() );
            Container.Bind<ITreasureSetPM_Spawner>().FromInstance( Substitute.For<ITreasureSetPM_Spawner>() );
            Container.Bind<AllTreasurePM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IsVisible_IsFalse_ByDefault() {
            systemUnderTest.Initialize();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( AllTreasurePM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void OnShow_IsVisible_IsTrue() {
            systemUnderTest.ViewModel.SetProperty( AllTreasurePM.VISIBLE_PROPERTY, false );
            systemUnderTest.Show();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( AllTreasurePM.VISIBLE_PROPERTY );
            Assert.IsTrue( isVisible );
        }

        [Test]
        public void OnHide_IsVisible_IsFalse() {
            systemUnderTest.ViewModel.SetProperty( AllTreasurePM.VISIBLE_PROPERTY, true );
            systemUnderTest.Hide();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( AllTreasurePM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void WhenInited_TreasureSetPMs_MatchDataManager() {
            List<ITreasureSetData> mockTreasureSets = new List<ITreasureSetData>();
            mockTreasureSets.Add( Substitute.For<ITreasureSetData>() );
            mockTreasureSets.Add( Substitute.For<ITreasureSetData>() );
            mockTreasureSets.Add( Substitute.For<ITreasureSetData>() );
            TreasureDataManager.TreasureSetData.Returns( mockTreasureSets );
            systemUnderTest.Initialize();

            Assert.AreEqual( 3, systemUnderTest.TreasureSetPMs.Count );
        }
    }
}
