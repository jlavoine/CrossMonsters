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
    public class TestPlayerStatInfoPM : MonoBehaviour {
        [Test]
        public void WhenCreated_StatsSetAsExpected() {
            IPlayerDataManager mockManager = Substitute.For<IPlayerDataManager>();
            mockManager.GetStat( PlayerStats.HP ).Returns( 100 );
            mockManager.GetStat( PlayerStats.PHY_ATK ).Returns( 10 );
            mockManager.GetStat( PlayerStats.PHY_DEF ).Returns( 5 );

            PlayerStatInfoPM systemUnderTest = new PlayerStatInfoPM(mockManager);

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerStatInfoPM.HP_PROPERTY ) );
            Assert.AreEqual( "10", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerStatInfoPM.ATK_PROPERTY ) );
            Assert.AreEqual( "5", systemUnderTest.ViewModel.GetPropertyValue<string>( PlayerStatInfoPM.DEF_PROPERTY ) );
        }
    }
}
