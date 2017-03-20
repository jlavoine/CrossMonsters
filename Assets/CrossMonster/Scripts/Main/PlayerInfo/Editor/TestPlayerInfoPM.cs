using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestPlayerInfoPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_GoldSetToExpectedValue() {
            IPlayerDataManager mockManager = Substitute.For<IPlayerDataManager>();
            mockManager.Gold.Returns( 100 );

            PlayerInfoPM systemUnderTest = new PlayerInfoPM( mockManager );

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerInfoPM.GOLD_PROPERTY ) );
        }
    }
}