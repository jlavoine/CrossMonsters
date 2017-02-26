using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace CrossMonsters {
    [TestFixture]
    public class TestAttackComboPiecePM : CrossMonstersUnitTest {

        [Test]
        public void WhenCreating_PieceTypeProperty_MatchesIncomingType() {
            AttackComboPiecePM systemUnderTest = new AttackComboPiecePM( 3 );

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( AttackComboPiecePM.PIECE_TYPE_PROPERTY ) );
        }
    }
}