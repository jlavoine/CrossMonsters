using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestMonsterManager : CrossMonstersUnitTest {

        [Test]
        public void WhenManagerTicks_AllMonstersTick() {
            List<IGameMonster> mockMonsters = new List<IGameMonster>();
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );
            mockMonsters.Add( Substitute.For<IGameMonster>() );

            MonsterManager systemUnderTest = new MonsterManager();
            systemUnderTest.CurrentMonsters = mockMonsters;

            systemUnderTest.Tick( 1000 );

            foreach ( IGameMonster monster in systemUnderTest.CurrentMonsters ) {
                monster.Received().Tick( 1000 );
            }
        }
    }
}