using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePlayer : CrossMonstersUnitTest {

        [Test]
        public void PlayerHP_MatchesData() {
            IPlayerData mockData = Substitute.For<IPlayerData>();
            mockData.GetHP().Returns( 101 );
            GamePlayer systemUnderTest = new GamePlayer( mockData );

            Assert.AreEqual( 101, systemUnderTest.HP );
        }
    }
}