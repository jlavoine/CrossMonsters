using System.Collections.Generic;

namespace MyLibrary {
    public class NewsManager : INewsManager {
        private IBasicBackend mBackend;

        private List<IBasicNewsData> mNewsList;
        public List<IBasicNewsData> NewsList { get { return mNewsList; } set { mNewsList = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadNews();
        }

        private void DownloadNews() {
            NewsList = new List<IBasicNewsData>();

            mBackend.GetNews( ( result ) => {
                NewsList = result;
            } );
        }
    }
}
