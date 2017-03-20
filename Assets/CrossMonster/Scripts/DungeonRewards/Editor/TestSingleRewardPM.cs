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

        [Test]
        public void WhenUncovered_CoverVisibility_IsFalse() {
            SingleRewardPM systemUnderTest = new SingleRewardPM();
            systemUnderTest.ViewModel.SetProperty( SingleRewardPM.COVER_VISIBLE_PROPERTY, true );

            systemUnderTest.UncoverReward();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( SingleRewardPM.COVER_VISIBLE_PROPERTY ) );
        }
    }
}