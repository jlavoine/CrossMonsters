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
    public class TestShowGauntletStep : MonoBehaviour {
        private ICurrentGauntletManager MockGauntletManager;
        private IEnterGauntletPM MockEnterGauntlet;

        [SetUp]
        public void CommonInstall() {
            MockGauntletManager = Substitute.For<ICurrentGauntletManager>();
            MockEnterGauntlet = Substitute.For<IEnterGauntletPM>();
        }

        private ShowGauntletStep CreateSystem() {
            ShowGauntletStep systemUnderTest = new ShowGauntletStep( MockGauntletManager, MockEnterGauntlet, Substitute.For<ISceneStartFlowManager>() );
            return systemUnderTest;
        }

        [Test]
        public void WhenComingFromGauntletVictory_EnterGauntIsShown() {
            MockGauntletManager.ComingFromGauntletVictory = true;
            MockGauntletManager.CurrentGauntletIndex = 11;
            ShowGauntletStep systemUnderTest = CreateSystem();

            systemUnderTest.Start();

            MockEnterGauntlet.Received().SetIndex( 11 );
            MockEnterGauntlet.Received().Show();
        }

        [Test]
        public void WhenNotComingFromGauntletVictory_EnterGauntIsNotShown() {
            MockGauntletManager.ComingFromGauntletVictory = false;
            ShowGauntletStep systemUnderTest = CreateSystem();

            systemUnderTest.Start();

            MockEnterGauntlet.DidNotReceive().Show();
        }

        [Test]
        public void WhenComingFromGauntletVictory_VictoryIsMarkedFalse() {
            MockGauntletManager.ComingFromGauntletVictory = true;
            ShowGauntletStep systemUnderTest = CreateSystem();

            systemUnderTest.Start();

            MockGauntletManager.Received().ComingFromGauntletVictory = false;
        }
    }
}
