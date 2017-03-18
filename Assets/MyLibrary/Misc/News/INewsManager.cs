using System.Collections.Generic;

namespace MyLibrary {
    public interface INewsManager {
        void Init( IBasicBackend i_backend );
        void UpdateLastSeenNewsTime();

        bool ShouldShowNews();

        List<IBasicNewsData> NewsList { get; }
    }
}
