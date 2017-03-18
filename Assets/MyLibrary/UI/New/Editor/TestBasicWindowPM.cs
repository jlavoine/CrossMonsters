using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MyLibrary {
    [TestFixture]
    public class TestBasicWindowPM : MonoBehaviour {
        public class StubBasicWindow : BasicWindowPM { }

        [Test]
        public void IsVisible_IsFalse_ByDefault() {
            StubBasicWindow systemUnderTest = new StubBasicWindow();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( BasicWindowPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void OnShow_IsVisible_IsTrue() {
            StubBasicWindow systemUnderTest = new StubBasicWindow();
            systemUnderTest.ViewModel.SetProperty( BasicWindowPM.VISIBLE_PROPERTY, false );
            systemUnderTest.Show();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( BasicWindowPM.VISIBLE_PROPERTY );
            Assert.IsTrue( isVisible );
        }

        [Test]
        public void OnHide_IsVisible_IsFalse() {
            StubBasicWindow systemUnderTest = new StubBasicWindow();
            systemUnderTest.ViewModel.SetProperty( BasicWindowPM.VISIBLE_PROPERTY, true );
            systemUnderTest.Hide();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( BasicWindowPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }
    }
}