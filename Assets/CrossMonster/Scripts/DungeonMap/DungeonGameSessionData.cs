using System.Collections.Generic;

namespace CrossMonsters {
    public class DungeonGameSessionData : IDungeonGameSessionData {
        public List<string> Monsters;
        List<DungeonRewardData> Rewards;

        public bool AllowDiagonalMoves;
        public bool StraightLinesOnly;
        public bool ShouldRotatePiecesAfterUse;   

        public List<string> GetMonsters() {
            return Monsters;
        }

        public List<IDungeonRewardData> GetRewards() {
            List<IDungeonRewardData> rewards = new List<IDungeonRewardData>();

            // TEMP
            Rewards = new List<DungeonRewardData>();
            Rewards.Add( new DungeonRewardData() { Count = 100, Id = "Gold", LootType = LootTypes.Gold } );
            Rewards.Add( new DungeonRewardData() { Count = 50, Id = "Gold", LootType = LootTypes.Gold } );

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
