using System.Collections.Generic;

namespace CrossMonsters {
    public interface ICurrentDungeonGameManager {
        void SetData( IDungeonGameSessionData i_data );

        IDungeonGameSessionData Data { get; }
        List<IGameMonster> Monsters { get; }
    }
}
