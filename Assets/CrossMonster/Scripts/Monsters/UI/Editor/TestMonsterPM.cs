using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestMonsterPM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_PropertiesSetAsExpected() {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.Id.Returns( "Blob" );
            mockMonster.RemainingHP = 100;

            MonsterPM systemUnderTest = new MonsterPM( mockMonster );

            Assert.AreEqual( "Blob", systemUnderTest.ViewModel.GetPropertyValue<string>( MonsterPM.ID_PROPERTY ) );
            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( MonsterPM.HP_PROPERTY ) );
        }
    }
}
