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
    public class TestDungeonWavePM : ZenjectUnitTestFixture {
        [Inject]
        IGameManager MockManager;

        [Inject]
        DungeonWavePM systemUnderTest;

        [SetUp]
        public void CommonInstall() {           
            Container.Bind<IGameManager>().FromInstance( Substitute.For<IGameManager>() );
            Container.Bind<DungeonWavePM>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenShowIsCalled_AnimationStateIsShow() {
            systemUnderTest.Show();

            Assert.AreEqual( DungeonWavePM.SHOW_TRIGGER, systemUnderTest.ViewModel.GetPropertyValue<string>( DungeonWavePM.TRIGGER_PROPERTY ) );
        }

        [Test]
        public void WhenShowIsCalled_WaveLabelIsSet() {
            string expectedText = "Wave 2 of 3";
            systemUnderTest.CurrentWaveIndex = 1;
            systemUnderTest.EndWaveIndex = 3;
            systemUnderTest.Show();

            Assert.AreEqual( expectedText, systemUnderTest.ViewModel.GetPropertyValue<string>( DungeonWavePM.WAVE_TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenHideIsCalled_AnimationStateIsShow() {
            systemUnderTest.Hide();

            Assert.AreEqual( DungeonWavePM.HIDE_TRIGGER, systemUnderTest.ViewModel.GetPropertyValue<string>( DungeonWavePM.TRIGGER_PROPERTY ) );
        }

        [Test]
        public void WhenHideIsCalled_WaveManagerIsAlerted() {
            systemUnderTest.Hide();

            MockManager.Received().BeginWaveGameplay();
        }

        [Test]
        public void WhenSettingEndWaveIndex_IndexIsSet() {
            systemUnderTest.SetEndWave( 5 );

            Assert.AreEqual( 5, systemUnderTest.EndWaveIndex );
        }

        [Test]
        public void CurrentWaveIndex_StartsAtZero() {
            Assert.AreEqual( 0, systemUnderTest.CurrentWaveIndex );
        }

        [Test]
        public void WhenWaveUI_IsTriggered_CurrentWaveIndexIsIncremented() {
            systemUnderTest.CurrentWaveIndex = 2;
            systemUnderTest.Show();

            Assert.AreEqual( 3, systemUnderTest.CurrentWaveIndex );
        }
    }
}
