using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestTimedChestMainPM : ZenjectUnitTestFixture {

        [Test]
        public void IsVisible_IsFalse_ByDefault() {
            TimedChestsMainPM systemUnderTest = new TimedChestsMainPM( Substitute.For<ITimedChestDataManager>(), Substitute.For<ITimedChestPM_Spawner>() );
            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( BasicWindowPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void WhenInited_CreatedPMs_MatchDataManager() {
            ITimedChestDataManager mockDataManager = Substitute.For<ITimedChestDataManager>();
            List<ITimedChestData> mockData = new List<ITimedChestData>();
            mockData.Add( Substitute.For<ITimedChestData>() );
            mockData.Add( Substitute.For<ITimedChestData>() );
            mockData.Add( Substitute.For<ITimedChestData>() );
            mockDataManager.TimedChestData.Returns( mockData );

            TimedChestsMainPM systemUnderTest = new TimedChestsMainPM( mockDataManager, Substitute.For<ITimedChestPM_Spawner>() );

            Assert.AreEqual( 3, systemUnderTest.ChestPMs.Count );
        }
    }
}
