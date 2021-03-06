﻿using MyLibrary;

namespace MonsterMatch {
    public interface ISingleRewardPM_Spawner {
        ISingleRewardPM Create( IDungeonReward i_reward, IAllRewardsPM i_pm );
        ISingleRewardPM CreateEmpty();
    }

    public class SingleRewardPM_Spawner : ISingleRewardPM_Spawner {
        readonly SingleRewardPM.Factory factory;

        public SingleRewardPM_Spawner( SingleRewardPM.Factory i_factory ) {
            this.factory = i_factory;
        }

        public ISingleRewardPM Create( IDungeonReward i_reward, IAllRewardsPM i_pm ) {
            return factory.Create( i_reward, i_pm );
        }

        public ISingleRewardPM CreateEmpty() {
            IDungeonReward emptyReward = null;
            IAllRewardsPM emptyRewardsPM = null;
            return factory.Create( emptyReward, emptyRewardsPM );
        }
    }
}