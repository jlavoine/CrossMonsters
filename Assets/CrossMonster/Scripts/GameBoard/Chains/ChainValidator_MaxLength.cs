using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class ChainValidator_MaxLength : IChainValidator_MaxLength {
        [Inject]
        IMonsterManager MonsterManager;

        public bool IsValidPieceInChain( IGamePiece i_piece, List<IGamePiece> i_chain ) {
            int longestCombo = MonsterManager.GetLongestComboFromCurrentWave();
            return i_chain.Count < longestCombo;
        }
    }
}