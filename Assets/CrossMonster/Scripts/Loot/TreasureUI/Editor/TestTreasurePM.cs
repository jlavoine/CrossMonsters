using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace MonsterMatch {
    public class TestTreasurePM : ZenjectUnitTestFixture {
        
        [Test]
        public void WhenCreating_TreasureVisibility_IsTrue_IfPlayerHasTreasure() {
            ITreasureDataManager mockManager = Substitute.For<ITreasureDataManager>();
            mockManager.DoesPlayerHaveTreasure( Arg.Any<string>() ).Returns( true );
            TreasurePM systemUnderTest = new TreasurePM( mockManager, "FakeTreasureID" );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( TreasurePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_TreasureVisibility_IsFalse_IfPlayerDoesNotHaveTreasure() {
            ITreasureDataManager mockManager = Substitute.For<ITreasureDataManager>();
            mockManager.DoesPlayerHaveTreasure( Arg.Any<string>() ).Returns( false );
            TreasurePM systemUnderTest = new TreasurePM( mockManager, "FakeTreasureID" );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( TreasurePM.VISIBLE_PROPERTY ) );
        }
    }
}
