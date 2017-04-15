using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestMonsterWaveData : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_MonstersListIsEmpty() {
            MonsterWaveData systemUnderTest = new MonsterWaveData();

            Assert.IsEmpty( systemUnderTest.Monsters );
        }

        [Test]
        public void WhenAddingMonster_MonsterIsAddedToList() {
            MonsterWaveData systemUnderTest = new MonsterWaveData();
            IGameMonster mockMonster = Substitute.For<IGameMonster>();

            systemUnderTest.AddMonster( mockMonster );

            Assert.Contains( mockMonster, systemUnderTest.Monsters );
        }
    }
}
