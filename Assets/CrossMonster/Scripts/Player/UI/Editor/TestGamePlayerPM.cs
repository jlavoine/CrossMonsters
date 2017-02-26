using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestGamePlayerPM : CrossMonstersUnitTest {
        [Test]
        public void HpProperty_MatchesGamePlayerHP() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.HP.Returns( 100 );
            GamePlayerPM systemUnderTest = new GamePlayerPM( mockPlayer );

            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( GamePlayerPM.HP_PROPERTY ) );
        }
    }
}
