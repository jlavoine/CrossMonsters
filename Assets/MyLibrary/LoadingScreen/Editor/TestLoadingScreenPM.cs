using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestLoadingScreenPM {
        [Test]
        public void IsVisibleProperty_FalseByDefault() {
            LoadingScreenPM systemUnderTest = new LoadingScreenPM( Substitute.For<IStringTableManager>() );

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( LoadingScreenPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void OnShow_IsVisibleTrue() {
            LoadingScreenPM systemUnderTest = new LoadingScreenPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.Show();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( LoadingScreenPM.VISIBLE_PROPERTY );
            Assert.IsTrue( isVisible );
        }
    }
}
