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
    public class TestChainValidator : ZenjectUnitTestFixture {
        [Inject]
        ChainValidator systemUnderTest;

        [Inject]
        IChainValidator_DuplicatePieces MockDuplicatePieceValidator;

        [Inject]
        IChainValidator_DiagonalPieces MockDiagonalPieceValidator;

        [Inject]
        IChainValidator_StraightLinesOnly MockStraightLinesOnlyValidator;

        [Inject]
        IChainValidator_MaxLength MockMaxLengthValidator;

        [Inject]
        IChainValidator_SelectablePieces MockSelectablePieceValidator;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IChainValidator_DuplicatePieces>().FromInstance( Substitute.For<IChainValidator_DuplicatePieces>() );
            Container.Bind<IChainValidator_DiagonalPieces>().FromInstance( Substitute.For<IChainValidator_DiagonalPieces>() );
            Container.Bind<IChainValidator_StraightLinesOnly>().FromInstance( Substitute.For<IChainValidator_StraightLinesOnly>() );
            Container.Bind<IChainValidator_MaxLength>().FromInstance( Substitute.For<IChainValidator_MaxLength>() );
            Container.Bind<IChainValidator_SelectablePieces>().FromInstance( Substitute.For<IChainValidator_SelectablePieces>() );
            Container.Bind<ChainValidator>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void IfJustDuplicatePieceValidationFails_ValidationFails() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfJustDiagonalPieceValidationFails_ValidationFails() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfJustStraightLinesOnlyValidationFails_ValidationFails() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfJustMaxLengthValidationFails_ValidationFails() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfJustSelectablePieceValidationFails_ValidationFails() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( false );

            Assert.IsFalse( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }

        [Test]
        public void IfAllValidationPasses_Validation_Passes() {
            MockDuplicatePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockDiagonalPieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockStraightLinesOnlyValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockMaxLengthValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );
            MockSelectablePieceValidator.IsValidPieceInChain( Arg.Any<IGamePiece>(), Arg.Any<List<IGamePiece>>() ).Returns( true );

            Assert.IsTrue( systemUnderTest.IsValidPieceInChain( Substitute.For<IGamePiece>(), new List<IGamePiece>() ) );
        }
    }
}