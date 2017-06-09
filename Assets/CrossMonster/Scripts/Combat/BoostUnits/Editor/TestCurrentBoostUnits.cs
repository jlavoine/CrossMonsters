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
    public class TestCurrentBoostUnits : ZenjectUnitTestFixture {

        [Test]
        public void GetEffectValue_ChecksAndGetsEffect_FromUnits() {
            CurrentBoostUnits systemUnderTest = new CurrentBoostUnits();
            IBoostUnit unit1 = Substitute.For<IBoostUnit>();
            IBoostUnit unit2 = Substitute.For<IBoostUnit>();
            IBoostUnit unit3 = Substitute.For<IBoostUnit>();

            unit1.HasEffect( Arg.Any<string>() ).Returns( true );
            unit2.HasEffect( Arg.Any<string>() ).Returns( false );
            unit3.HasEffect( Arg.Any<string>() ).Returns( true );

            unit1.GetEffect( Arg.Any<string>() ).Returns( 1 );
            unit2.GetEffect( Arg.Any<string>() ).Returns( 2 );
            unit3.GetEffect( Arg.Any<string>() ).Returns( 3 );

            systemUnderTest.Units = new List<IBoostUnit>() { unit1, unit2, unit3 };

            int value = systemUnderTest.GetEffectValue( "Test" );

            Assert.AreEqual( value, 4 ); // 1 + 3 from unit1 and unit3
            unit1.Received().HasEffect( "Test" );
            unit2.Received().HasEffect( "Test" );
            unit3.Received().HasEffect( "Test" );

            unit1.Received().GetEffect( "Test" );
            unit2.DidNotReceive().GetEffect( "Test" );
            unit3.Received().GetEffect( "Test" );
        }
    }
}
