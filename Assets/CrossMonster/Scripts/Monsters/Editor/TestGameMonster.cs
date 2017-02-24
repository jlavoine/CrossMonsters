using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestGameMonster {
        [Test]
        public void WhenCreating_RemainingHP_EqualsMaxHP() {
            IMonsterData mockData = Substitute.For<IMonsterData>();
            mockData.GetMaxHP().Returns( 101 );

            GameMonster systemUnderTest = new GameMonster( mockData );

            Assert.AreEqual( 101, systemUnderTest.RemainingHP );
        }
    }
}