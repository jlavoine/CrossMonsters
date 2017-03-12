using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public interface IMonsterDataManager {
        void Init( IBasicBackend i_backend );

        IMonsterData GetData( string i_id );
    }
}