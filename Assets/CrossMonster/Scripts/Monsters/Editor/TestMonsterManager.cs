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

        [Test]
        public void WhenProcessingPlayerMove_AnyMatchingCurrentMonsters_GetAttacked() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<int> mockMove = new List<int>();
            MonsterManager systemUnderTest = new MonsterManager();

            List<IGameMonster> mockCurrentMonsters = new List<IGameMonster>();
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( true ) );
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( false ) );
            mockCurrentMonsters.Add( GetMockMonsterWithMatchCombo( false ) );

            List<IGameMonster> mockRemainingMonsters = new List<IGameMonster>();
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( true ) );
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( false ) );
            mockRemainingMonsters.Add( GetMockMonsterWithMatchCombo( false ) );

            systemUnderTest.CurrentMonsters = mockCurrentMonsters;
            systemUnderTest.RemainingMonsters = mockRemainingMonsters;

            systemUnderTest.ProcessPlayerMove( mockPlayer, mockMove );

            mockCurrentMonsters[0].Received( 1 ).AttackedByPlayer( mockPlayer );
            mockCurrentMonsters[1].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockCurrentMonsters[2].Received( 0 ).AttackedByPlayer( mockPlayer );

            mockRemainingMonsters[0].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockRemainingMonsters[1].Received( 0 ).AttackedByPlayer( mockPlayer );
            mockRemainingMonsters[2].Received( 0 ).AttackedByPlayer( mockPlayer );
        }

        private IGameMonster GetMockMonsterWithMatchCombo( bool i_doesMatch ) {
            IGameMonster mockMonster = Substitute.For<IGameMonster>();
            mockMonster.DoesMatchCombo( Arg.Any<List<int>>() ).Returns( i_doesMatch );

            return mockMonster;
        }
    }
}