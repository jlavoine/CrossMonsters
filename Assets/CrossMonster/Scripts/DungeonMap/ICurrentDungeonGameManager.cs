using System.Collections.Generic;

namespace MonsterMatch {
    public interface ICurrentDungeonGameManager {
        void SetData( IDungeonGameSessionData i_data );

        void AwardRewards();
        void Clear();

        IDungeonGameSessionData Data { get; }
        List<IMonsterWaveData> Monsters { get; }
        List<IDungeonReward> Rewards { get; }
    }
}
