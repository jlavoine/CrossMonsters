using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestDungeonRewardData {

        [Test]
        public void GetNameKey_ReturnsAsExpected() {
            DungeonRewardData systemUnderTest = new DungeonRewardData();
            systemUnderTest.Id = "Test";

            Assert.AreEqual( "Test" + DungeonRewardData.NAME_KEY, systemUnderTest.GetNameKey() );
        }
    }
}
