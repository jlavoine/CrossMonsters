using System.Collections.Generic;

namespace MonsterMatch {
    public interface IChainProcessor {
        void Process( List<IGamePiece> i_chain );
    }
}
