using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public interface IMonsterDataManager {
        void Init( IBasicBackend i_backend );

        IMonsterData GetData( string i_id );
    }
}