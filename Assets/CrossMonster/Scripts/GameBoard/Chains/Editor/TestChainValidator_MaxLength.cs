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
    public class TestChainValidator_MaxLength : ZenjectUnitTestFixture {

        [Inject]
        ChainValidator_MaxLength systemUnderTest;

        [Inject]
        IMonsterManager MockMonsterManager;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterManager>().FromInstance( Substitute.For<IMonsterManager>() );
            Container.Bind<ChainValidator_MaxLength>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IsValidPiece_IsTrue_IfIncomingListCount_LessThanMaxComboLength() {
            MockMonsterManager.GetLongestComboFromCurrentWave().Returns( 3 );
            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), GetMockComboListWithCount( 2 ) );

            Assert.IsTrue( isValid );
        }

        [Test]
        public void IsValidPiece_IsFalse_IfIncomingListCount_EqualToMaxComboLength() {
            MockMonsterManager.GetLongestComboFromCurrentWave().Returns( 3 );
            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), GetMockComboListWithCount( 3 ) );

            Assert.IsFalse( isValid );
        }

        [Test]
        public void IsValidPiece_IsFalse_IfIncomingListCount_GreaterThanMaxComboLength() {
            MockMonsterManager.GetLongestComboFromCurrentWave().Returns( 3 );
            bool isValid = systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), GetMockComboListWithCount( 4 ) );

            Assert.IsFalse( isValid );
        }

        private List<IGamePiece> GetMockComboListWithCount( int i_count ) {
            List<IGamePiece> list = new List<IGamePiece>();
            for ( int i = 0; i < i_count; ++i ) {
                list.Add( Substitute.For<IGamePiece>() );
            }

            return list;
        }
    }
}