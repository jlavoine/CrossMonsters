using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    [TestFixture]
    public class TestSingleLoginPromoRewardPM : ZenjectUnitTestFixture {

        private IDungeonRewardSpawner MockDungeonRewardSpawner;
        private ISingleRewardPM_Spawner MockRewardPMSpawner;
        private IGameRewardData MockRewardData;

        [SetUp]
        public void CommonInstall() {
            MockDungeonRewardSpawner = Substitute.For<IDungeonRewardSpawner>();
            MockRewardPMSpawner = Substitute.For<ISingleRewardPM_Spawner>();
            MockRewardData = Substitute.For<IGameRewardData>();
        }

        [Test]
        public void WhenCreated_RewardNumberProperty_MatchesIncomingNumber() {
            SingleLoginPromoRewardPM systemUnderTest = CreateSystem( 4 );

            Assert.AreEqual( "4", systemUnderTest.ViewModel.GetPropertyValue<string>( SingleLoginPromoRewardPM.REWARD_NUMBER_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_DungeonRewardIsCreatedWithRewardData() {
            SingleLoginPromoRewardPM systemUnderTest = CreateSystem( 0 );

            MockDungeonRewardSpawner.Received().Create( MockRewardData );
        }

        [Test]
        public void WhenCreated_RewardPM_IsCreatedWithSpawnedDungeonReward() {
            IDungeonReward mockDungeonReward = Substitute.For<IDungeonReward>();
            MockDungeonRewardSpawner.Create( Arg.Any<IGameRewardData>() ).Returns( mockDungeonReward );

            SingleLoginPromoRewardPM systemUnderTest = CreateSystem( 0 );

            MockRewardPMSpawner.Received().Create( mockDungeonReward, null );
        }
        
        [Test]
        public void WhenCreated_SpawnedRewardPM_IsUncovered() {
            ISingleRewardPM mockRewardPM = Substitute.For<ISingleRewardPM>();
            MockRewardPMSpawner.Create( Arg.Any<IDungeonReward>(), Arg.Any<IAllRewardsPM>() ).Returns( mockRewardPM );

            SingleLoginPromoRewardPM systemUnderTest = CreateSystem( 0 );

            mockRewardPM.Received().UncoverReward();
        }

        private SingleLoginPromoRewardPM CreateSystem( int i_rewardNumber ) {
            SingleLoginPromoRewardPM systemUnderTest = new SingleLoginPromoRewardPM( MockDungeonRewardSpawner, MockRewardPMSpawner, i_rewardNumber, MockRewardData );
            return systemUnderTest;
        }
    }
}
