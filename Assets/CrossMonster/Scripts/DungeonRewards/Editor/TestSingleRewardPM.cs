using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestSingleRewardPM : ZenjectUnitTestFixture {
        
        [Test]
        public void WhenCreating_CoverIsVisibleByDefault() {
            SingleRewardPM systemUnderTest = new SingleRewardPM();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }
    }
}