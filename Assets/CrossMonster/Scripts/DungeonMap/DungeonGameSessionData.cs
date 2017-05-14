﻿using System.Collections.Generic;
using MyLibrary;

namespace MonsterMatch {
    public class DungeonGameSessionData : IDungeonGameSessionData {
        public List<string> Monsters;
        public List<GameRewardData> Rewards;

        public bool AllowDiagonalMoves;
        public bool StraightLinesOnly;
        public bool ShouldRotatePiecesAfterUse;   

        public List<string> GetMonsters() {
            return Monsters;
        }

        public List<IGameRewardData> GetRewards() {
            List<IGameRewardData> rewards = new List<IGameRewardData>();
            foreach ( GameRewardData rewardData in Rewards ) {
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
