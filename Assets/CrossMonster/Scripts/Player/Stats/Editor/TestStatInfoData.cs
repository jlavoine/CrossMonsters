using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestStatInfoData : ZenjectUnitTestFixture {

        [Test]
        public void GetValuePerLevel_ReturnsValueOfStat() {
            StatInfoData systemUnderTest = new StatInfoData();
            systemUnderTest.Stats = new Dictionary<string, StatInfoEntry>();
            systemUnderTest.Stats.Add( "TestStat", new StatInfoEntry() { ValuePerLevel = 2f } );

            Assert.AreEqual( 2f, systemUnderTest.GetValuePerLevel( "TestStat" ) );
        }

        [Test]
        public void GetValuePerLevel_ReturnsZero_IfStatDoesNotExist() {
            StatInfoData systemUnderTest = new StatInfoData();
            systemUnderTest.Stats = new Dictionary<string, StatInfoEntry>();
            systemUnderTest.Stats.Add( "TestStat", new StatInfoEntry() { ValuePerLevel = 2f } );

            Assert.AreEqual( 0f, systemUnderTest.GetValuePerLevel( "WrongStat" ) );
        }
    }
}