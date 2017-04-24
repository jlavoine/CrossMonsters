using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestGameOverPM : CrossMonstersUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            GameOverPM systemUnderTest = new GameOverPM( Substitute.For<IStringTableManager>() );

            MyMessenger.Instance.Received().AddListener<bool>( GameMessages.GAME_OVER, Arg.Any<Callback<bool>>() );            
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            GameOverPM systemUnderTest = new GameOverPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<bool>( GameMessages.GAME_OVER, Arg.Any<Callback<bool>>() );
        }

        [Test]
        public void IsVisibleProperty_FalseByDefault() {
            GameOverPM systemUnderTest = new GameOverPM( Substitute.For<IStringTableManager>() );

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( GameOverPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void AfterGameOverMessage_IfLoss_IsVisibleTrue() {
            GameOverPM systemUnderTest = new GameOverPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.OnGameOver( false );

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( GameOverPM.VISIBLE_PROPERTY );
            Assert.IsTrue( isVisible );
        }

        [Test]
        public void AfterGameOverMessage_IfWin_IsVisibleFalse() {
            GameOverPM systemUnderTest = new GameOverPM( Substitute.For<IStringTableManager>() );

            systemUnderTest.OnGameOver( true );

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( GameOverPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        static object[] BodyTextTests = {
            new object[] { "You lose", GameOverPM.LOST_GAME_KEY, false }
        };

        [Test, TestCaseSource("BodyTextTests")]
        public void AfterGameOverMessage_BodyTextPropertyAsExpected( string i_expectedMessage, string i_key, bool i_won ) {
            IStringTableManager mockStringTableManager = Substitute.For<IStringTableManager>();
            mockStringTableManager.Get( i_key ).Returns( i_expectedMessage );

            GameOverPM systemUnderTest = new GameOverPM( mockStringTableManager );
            systemUnderTest.OnGameOver( i_won );

            Assert.AreEqual( i_expectedMessage, systemUnderTest.ViewModel.GetPropertyValue<string>( GameOverPM.BODY_TEXT_PROPERTY ) );
        }
    }
}