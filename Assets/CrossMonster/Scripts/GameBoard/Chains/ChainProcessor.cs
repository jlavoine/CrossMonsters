using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class ChainProcessor : IChainProcessor {
        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGamePlayer GamePlayer;

        public void Process( List<int> i_chain ) {
            MonsterManager.ProcessPlayerMove( GamePlayer, i_chain );
        }
    }
}
