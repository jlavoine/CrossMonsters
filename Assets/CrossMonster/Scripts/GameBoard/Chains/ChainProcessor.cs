using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainProcessor : IChainProcessor {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGamePlayer GamePlayer;

        public void Process( List<IGamePiece> i_chain ) {
            if ( MonsterManager.DoesMoveMatchAnyCurrentMonsters( i_chain ) ) {
                MonsterManager.ProcessPlayerMove( GamePlayer, i_chain );
                UsePiecesInChain( i_chain );
            }
        }

        private void UsePiecesInChain( List<IGamePiece> i_pieces ) {
            foreach ( IGamePiece piece in i_pieces ) {
                piece.UsePiece();
            }
        }
    }
}
