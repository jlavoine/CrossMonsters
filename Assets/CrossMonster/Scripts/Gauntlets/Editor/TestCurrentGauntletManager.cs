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
    public class TestCurrentGauntletManager : ZenjectUnitTestFixture {
        private ICurrentDungeonGameManager MockDungeonManager;

        [SetUp]
        public void CommonInstall() {
            MockDungeonManager = Substitute.For<ICurrentDungeonGameManager>();
        }

        private CurrentGauntletManager CreateSystem() {
            CurrentGauntletManager systemUnderTest = new CurrentGauntletManager( MockDungeonManager );
            return systemUnderTest;
        }

        [Test]
        public void IsGauntletInSession_ReturnsFalse_IfCurrentDungeonDataIsNull() {
            IDungeonGameSessionData mockData = null;
            MockDungeonManager.Data.Returns( mockData );

            CurrentGauntletManager systemUnderTest = CreateSystem();

            Assert.IsFalse( systemUnderTest.IsGauntletSessionInProgress() );
        }

        [Test]
        public void IsGauntletInSession_ReturnsTrue_IfCurrentDungeonModeIsGauntlet() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetGameMode().Returns( EnterGauntletPM.GAUNTLET_GAME_TYPE );
            MockDungeonManager.Data.Returns( mockData );

            CurrentGauntletManager systemUnderTest = CreateSystem();

            Assert.IsTrue( systemUnderTest.IsGauntletSessionInProgress() );
        }

        [Test]
        public void IsGauntletInSession_ReturnsFalse_IfCurrentDungeonModeIsNotGauntlet() {
            IDungeonGameSessionData mockData = Substitute.For<IDungeonGameSessionData>();
            mockData.GetGameMode().Returns( "NotGauntlet" );
            MockDungeonManager.Data.Returns( mockData );

            CurrentGauntletManager systemUnderTest = CreateSystem();

            Assert.IsFalse( systemUnderTest.IsGauntletSessionInProgress() );
        }
    }
}