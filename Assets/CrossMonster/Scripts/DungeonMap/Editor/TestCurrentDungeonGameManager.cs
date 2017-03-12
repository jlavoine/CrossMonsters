using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    public class TestCurrentDungeonGameManager : ZenjectUnitTestFixture {

        [Inject]
        IMonsterDataManager MockMonsterData;

        [Inject]
        CurrentDungeonGameManager systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterDataManager>().FromInstance( Substitute.For<IMonsterDataManager>() );            
            Container.Bind<CurrentDungeonGameManager>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenSettingData_DataIsSaved() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetMonsters().Returns( new List<string>() { "a", "b", "c" } );
            MockMonsterData.GetData( Arg.Any<string>() ).Returns( Substitute.For<IMonsterData>() );
            systemUnderTest.SetData( mockData );

            Assert.AreEqual( mockData, systemUnderTest.Data );
        }

        [Test]
        public void WhenSettingData_MonstersAreGenerated() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetMonsters().Returns( new List<string>() { "a", "b", "c" } );
            MockMonsterData.GetData( Arg.Any<string>() ).Returns( Substitute.For<IMonsterData>() );

            systemUnderTest.SetData( mockData );

            Assert.AreEqual( 3, systemUnderTest.Monsters.Count );
        }
    }
}