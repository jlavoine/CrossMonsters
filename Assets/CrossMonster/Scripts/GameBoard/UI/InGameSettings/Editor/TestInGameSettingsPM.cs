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
    public class TestInGameSettingsPM : ZenjectUnitTestFixture {

        private IGameManager MockGameManager;

        [SetUp]
        public void CommonInstall() {
            MockGameManager = Substitute.For<IGameManager>();
        }

        private InGameSettingsPM CreateSystem() {
            InGameSettingsPM systemUnderTest = new InGameSettingsPM( MockGameManager );
            return systemUnderTest;
        }

        [Test]
        public void WhenShown_GameIsPaused() {
            InGameSettingsPM systemUnderTest = CreateSystem();

            systemUnderTest.Show();

            MockGameManager.Received().SetState( GameStates.Paused );
        }

        [Test]
        public void WhenHidden_GameIsResumed() {
            InGameSettingsPM systemUnderTest = CreateSystem();

            systemUnderTest.Hide();

            MockGameManager.Received().SetState( GameStates.Playing );
        }
    }
}