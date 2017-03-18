using System.Collections.Generic;

namespace MyLibrary {
    public interface INewsManager {
        void Init( IBasicBackend i_backend );

        List<IBasicNewsData> NewsList { get; }
    }
}
