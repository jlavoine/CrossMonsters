using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestPlayerInfoPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_GoldSetToExpectedValue() {
            IPlayerDataManager mockManager = Substitute.For<IPlayerDataManager>();
            mockManager.Gold.Returns( 100 );

            PlayerSummaryPM systemUnderTest = new PlayerSummaryPM( mockManager );

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerSummaryPM.GOLD_PROPERTY ) );
        }
    }
}