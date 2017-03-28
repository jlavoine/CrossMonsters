using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0219
#pragma warning disable 0414

namespace CrossMonsters {
    [TestFixture]
    public class TestChainValidator : ZenjectUnitTestFixture {
        [Inject]
        ChainValidator systemUnderTest;

        [Inject]
        IChainValidator_DuplicatePieces MockDuplicatePieceValidation;

        [Inject]
        IChainValidator_DiagonalPieces MockDiagonalPieceValidation;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainValidator_DuplicatePieces>().FromInstance( Substitute.For<IChainValidator_DuplicatePieces>() );
            Container.Bind<IChainValidator_DiagonalPieces>().FromInstance( Substitute.For<IChainValidator_DiagonalPieces>() );
            Container.Bind<ChainValidator>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IfJustDuplicatePieceValidationFails_ValidationFails() {
            MockDuplicatePieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );
            MockDiagonalPieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfJustDiagonalPieceValidationFails_ValidationFails() {
            MockDuplicatePieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfAllValidationPasses_Validation_Passes() {
            MockDuplicatePieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidation.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsTrue( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }
    }
}