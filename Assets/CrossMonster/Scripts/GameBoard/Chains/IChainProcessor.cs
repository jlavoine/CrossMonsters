using System.Collections.Generic;

namespace CrossMonsters {
    public interface IChainProcessor {
        void Process( List<IGamePiece> i_chain );
    }
}
