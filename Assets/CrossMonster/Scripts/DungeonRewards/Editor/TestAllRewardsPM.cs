using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using Zenject;
using System.Collections.Generic;

#pragma warning disable 0219

namespace MonsterMatch {
    [TestFixture]
    public class TestAllRewardsPM : ZenjectUnitTestFixture {

        [Test]
        public void WhenCreating_ExpectedMessagesSubscribed() {
            IMessageService mockMessenger = Substitute.For<IMessageService>();
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), mockMessenger, new List<IDungeonReward>() );

            mockMessenger.Received().AddListener( GameMessages.GAME_OVER, Arg.Any<Callback<bool>>() );
        }

        public void WhenDisposing_ExpectedMessagesUnsubsribed() {
            IMessageService mockMessenger = Substitute.For<IMessageService>();
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), mockMessenger, new List<IDungeonReward>() );

            systemUnderTest.Dispose();

            mockMessenger.Received().RemoveListener( GameMessages.GAME_OVER, Arg.Any<Callback<bool>>() );
        }

        [Test]
        public void WhenCreating_Visibility_IsFalse() {
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), new List<IDungeonReward>() );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_CanContinue_IsFalse() {
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), new List<IDungeonReward>() );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.CAN_CONTINUE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SingleRewardPMs_AreCreatedFromIncomingData() {
            List<IDungeonReward> mockRewards = GetMockRewards( 3 );
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), mockRewards );

            Assert.AreEqual( 3, systemUnderTest.SingleRewardPMs.Count );
        }

        [Test]
        public void WhenCreating_CoveredRewardCount_EqualsIncomingRewardCount() {
            List<IDungeonReward> mockRewards = GetMockRewards( 3 );
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), mockRewards );

            Assert.AreEqual( mockRewards.Count, systemUnderTest.CoveredRewardCount );
        }

        [Test]
        public void WhenUncoveringReward_CoveredCountDecreases() {
            List<IDungeonReward> mockRewards = GetMockRewards( 3 );
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), mockRewards );

            systemUnderTest.RewardUncovered();

            Assert.AreEqual( mockRewards.Count-1, systemUnderTest.CoveredRewardCount );
        }

        [Test]
        public void WhenUncoveringReward_IfCoveredCountIsAboveZero_CanContinueIsFalse() {
            List<IDungeonReward> mockRewards = GetMockRewards( 3 );
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), mockRewards );

            systemUnderTest.RewardUncovered();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.CAN_CONTINUE_PROPERTY ) );
        }

        [Test]
        public void WhenUncoveringReward_IfCoveredCountIsZero_CanContinueIsTrue() {
            List<IDungeonReward> mockRewards = GetMockRewards( 1 );
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), mockRewards );

            systemUnderTest.RewardUncovered();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.CAN_CONTINUE_PROPERTY ) );
        }

        [Test]
        public void WhenPlayerWins_IsVisible_IsTrue() {
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), new List<IDungeonReward>() );
            systemUnderTest.ViewModel.SetProperty( AllRewardsPM.VISIBLE_PROPERTY, false );

            systemUnderTest.OnGameOver( true );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenPlayerLoses_IsVisible_IsFalse() {
            AllRewardsPM systemUnderTest = new AllRewardsPM( Substitute.For<ISingleRewardPM_Spawner>(), Substitute.For<IMessageService>(), new List<IDungeonReward>() );
            systemUnderTest.ViewModel.SetProperty( AllRewardsPM.VISIBLE_PROPERTY, false );

            systemUnderTest.OnGameOver( false );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( AllRewardsPM.VISIBLE_PROPERTY ) );
        }

        private List<IDungeonReward> GetMockRewards( int i_count ) {
            List<IDungeonReward> mockRewards = new List<IDungeonReward>();
            for ( int i = 0; i < i_count; ++i ) {
                mockRewards.Add( Substitute.For<IDungeonReward>() );
            }
            
            return mockRewards;
        }
    }
}