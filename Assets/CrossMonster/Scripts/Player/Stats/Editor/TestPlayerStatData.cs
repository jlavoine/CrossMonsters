using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestPlayerStatData : ZenjectUnitTestFixture {

        [Test]
        public void GetLevel_ReturnsLevelOfStat() {
            PlayerStatData systemUnderTest = new PlayerStatData();
            systemUnderTest.Stats = new Dictionary<string, PlayerStatEntry>();
            systemUnderTest.Stats.Add( "TestStat", new PlayerStatEntry() { Level = 2 } );

            Assert.AreEqual( 2, systemUnderTest.GetStatLevel( "TestStat" ) );
        }

        [Test]
        public void GetLevel_ReturnsZero_IfStatDoesNotExist() {
            PlayerStatData systemUnderTest = new PlayerStatData();
            systemUnderTest.Stats = new Dictionary<string, PlayerStatEntry>();
            systemUnderTest.Stats.Add( "TestStat", new PlayerStatEntry() { Level = 2 } );

            Assert.AreEqual( 0, systemUnderTest.GetStatLevel( "WrongStat" ) );
        }
    }
}