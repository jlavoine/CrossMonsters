﻿using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace MonsterMatch {
    public class ChainProcessor : IChainProcessor {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGamePlayer GamePlayer;

        [Inject]
        IAudioManager Audio;

        [Inject]
        IGameBoard GameBoard;

        public void Process( List<IGamePiece> i_chain ) {
            if ( MonsterManager.DoesMoveMatchAnyCurrentMonsters( i_chain ) ) {
                MonsterManager.ProcessPlayerMove( GamePlayer, i_chain );
                UsePiecesInChain( i_chain );
                RandomizeGameBoardIfNoMonsterCombosAvailable();
                Audio.PlayOneShot( CombatAudioKeys.CHAIN_COMPLETE );
            } else {
                Audio.PlayOneShot( CombatAudioKeys.CHAIN_BROKEN );
            }
        }

        private void UsePiecesInChain( List<IGamePiece> i_pieces ) {
            foreach ( IGamePiece piece in i_pieces ) {
                piece.UsePiece();
            }
        }

        private void RandomizeGameBoardIfNoMonsterCombosAvailable() {
            GameBoard.RandomizeGameBoardIfNoMonsterCombosAvailable();
        }
    }
}
