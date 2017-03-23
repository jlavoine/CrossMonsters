using System.Collections.Generic;

namespace CrossMonsters {
    public interface ICurrentDungeonGameManager {
        void SetData( IDungeonGameSessionData i_data );

        void AwardRewards();

        IDungeonGameSessionData Data { get; }
        List<IGameMonster> Monsters { get; }
        List<IDungeonReward> Rewards { get; }
    }
}
