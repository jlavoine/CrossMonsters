using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestEnterDungeonPM : ZenjectUnitTestFixture {

        private IDungeonLoader MockDungeonLoader;

        [SetUp]
        public void CommonInstall() {
            MockDungeonLoader = Substitute.For<IDungeonLoader>();
        }

        private EnterDungeonPM CreateSystem() {
            EnterDungeonPM systemUnderTest = new EnterDungeonPM( MockDungeonLoader );
            return systemUnderTest;
        }

        [Test]
        public void WhenSettingRequestedDungeon_AllPropertiesAreSet() {
            EnterDungeonPM systemUnderTest = CreateSystem();

            systemUnderTest.SetRequestedDungeon( "TestType", 11, 100 );

            Assert.AreEqual( "TestType", systemUnderTest.GameType );
            Assert.AreEqual( 11, systemUnderTest.AreaId );
            Assert.AreEqual( 100, systemUnderTest.DungeonId );
        }

        [Test]
        public void WhenLoadingDungeon_LoaderIsCalledWithExpectedSettings() {
            EnterDungeonPM systemUnderTest = CreateSystem();
            systemUnderTest.GameType = "Test";
            systemUnderTest.AreaId = 1;
            systemUnderTest.DungeonId = 2;

            systemUnderTest.LoadDungeon();

            MockDungeonLoader.Received().LoadDungeon( "Test", 1, 2 );
        }

        [Test]
        public void WhenLoadingDungeon_SystemIsHidden() {
            EnterDungeonPM systemUnderTest = CreateSystem();
            systemUnderTest.ViewModel.SetProperty( EnterDungeonPM.VISIBLE_PROPERTY, true );

            systemUnderTest.LoadDungeon();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( EnterDungeonPM.VISIBLE_PROPERTY ) );
        }
    }
}
