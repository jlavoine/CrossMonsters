using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainProcessor : IChainProcessor {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGamePlayer GamePlayer;

        public void Process( List<IGamePiece> i_chain ) {
            MonsterManager.ProcessPlayerMove( GamePlayer, i_chain );
        }
    }
}
