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
    public class TestChainProcessor : ZenjectUnitTestFixture {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGameBoard GameBoard;

        [Inject]
        ChainProcessor systemUnderTest;

        [SetUp]
        public void CommonInstall() {
            Container.Bind<IMonsterManager>().FromInstance( Substitute.For<IMonsterManager>() );
            Container.Bind<IGameBoard>().FromInstance( Substitute.For<IGameBoard>() );
            Container.Bind<IGamePlayer>().FromInstance( Substitute.For<IGamePlayer>() );
            Container.Bind<ChainProcessor>().AsSingle();
            Container.Inject( this );
        }

        [Test]
        public void WhenProcessingChain_IfChainMatchesAnyMonster_MonsterManagerProcesses() {
            MonsterManager.DoesMoveMatchAnyCurrentMonsters( Arg.Any<List<IGamePiece>>() ).Returns( true );
            List<IGamePiece> chain = new List<IGamePiece>();

            systemUnderTest.Process( chain );

            MonsterManager.Received().ProcessPlayerMove( Arg.Any<IGamePlayer>(), chain );
        }

        [Test]
        public void WhenProcessingChain_IfChainDoesNotMatchAnyMonster_MonsterManagerProcesses() {
            MonsterManager.DoesMoveMatchAnyCurrentMonsters( Arg.Any<List<IGamePiece>>() ).Returns( false );
            List<IGamePiece> chain = new List<IGamePiece>();

            systemUnderTest.Process( chain );

            MonsterManager.DidNotReceive().ProcessPlayerMove( Arg.Any<IGamePlayer>(), chain );
        }

        [Test]
        public void WhenProcessingChain_IfChainIsProcessed_PiecesInvolvedAreUsed() {
            MonsterManager.DoesMoveMatchAnyCurrentMonsters( Arg.Any<List<IGamePiece>>() ).Returns( true );
            List<IGamePiece> chain = new List<IGamePiece>();
            chain.Add( Substitute.For<IGamePiece>() );
            chain.Add( Substitute.For<IGamePiece>() );
            chain.Add( Substitute.For<IGamePiece>() );

            systemUnderTest.Process( chain );

            foreach ( IGamePiece piece in chain ) {
                piece.Received().UsePiece();
            }
        }

        [Test]
        public void WhenProcessingChain_IfChainIsProcessed_GameBoardIsCheckedToBeRandomized() {
            MonsterManager.DoesMoveMatchAnyCurrentMonsters( Arg.Any<List<IGamePiece>>() ).Returns( true );
            List<IGamePiece> chain = new List<IGamePiece>();

            systemUnderTest.Process( chain );

            GameBoard.Received().RandomizeGameBoardIfNoMonsterCombosAvailable();
        }
    }
}
