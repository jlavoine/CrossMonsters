using System.Collections.Generic;
using System;

namespace MyLibrary {
    public class NewsManager : INewsManager {
        public const string LAST_SEEN_NEWS_KEY = "LastNewsViewTime";

        private IBasicBackend mBackend;

        private List<IBasicNewsData> mNewsList = new List<IBasicNewsData>();
        public List<IBasicNewsData> NewsList { get { return mNewsList; } set { mNewsList = value; } }

        private DateTime mLastSeenNewsTime = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );
        public DateTime LastSeenNewsTime { get { return mLastSeenNewsTime; } set {mLastSeenNewsTime = value; } }

        public void Init( IBasicBackend i_backend ) {
            mBackend = i_backend;

            DownloadNews();
            DownloadLastNewsViewTime();
        }

        public bool ShouldShowNews() {
            if ( NewsList.Count > 0 ) {
                DateTime latestNewsTime = NewsList[0].GetTimestamp();
                bool shouldShow = latestNewsTime > LastSeenNewsTime;
                return shouldShow;
            } else {
                return false;
            }
        }

        public void UpdateLastSeenNewsTime() {
            LastSeenNewsTime = NewsList[0].GetTimestamp();
            mBackend.UpdatePlayerData( LAST_SEEN_NEWS_KEY, LastSeenNewsTime.Ticks.ToString() );
        }

        private void DownloadNews() {
            NewsList = new List<IBasicNewsData>();

            mBackend.GetNews( ( result ) => {
                NewsList = result;
            } );
        }

        private void DownloadLastNewsViewTime() {
            mBackend.GetPublicPlayerData( LAST_SEEN_NEWS_KEY, ( result ) => {
                long ticks = long.Parse( result );
                mLastSeenNewsTime = new DateTime( ticks );
            } );
        }
    }
}
