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
        AllTreasurePM systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<AllTreasurePM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IsVisible_IsFalse_ByDefault() {
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
    }
}
