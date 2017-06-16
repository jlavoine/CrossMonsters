using System.Collections.Generic;

namespace MonsterMatch {
    public interface IChainProcessor {
        string Process( List<IGamePiece> i_chain );
    }
}
