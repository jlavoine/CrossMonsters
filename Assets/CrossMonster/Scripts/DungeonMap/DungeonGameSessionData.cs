using System.Collections.Generic;

namespace CrossMonsters {
    public class DungeonGameSessionData : IDungeonGameSessionData {
        public List<string> Monsters;
        public List<DungeonRewardData> Rewards;

        public bool AllowDiagonalMoves;
        public bool StraightLinesOnly;
        public bool ShouldRotatePiecesAfterUse;   

        public List<string> GetMonsters() {
            return Monsters;
        }

        public List<IDungeonRewardData> GetRewards() {
            List<IDungeonRewardData> rewards = new List<IDungeonRewardData>();
            foreach ( DungeonRewardData rewardData in Rewards ) {
                rewards.Add( rewardData );
            }

            return rewards;
        }

        public bool AllowDiagonals() {
            return AllowDiagonalMoves;
        }

        public bool StraightMovesOnly() {
            return StraightLinesOnly;
        }

        public bool ShouldRotatePieces() {
            return ShouldRotatePiecesAfterUse;
        }
    }
}
