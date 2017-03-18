using PlayFab.ClientModels;
using System;

namespace MyLibrary {
    public class BasicNewsData : IBasicNewsData {

        public string Title;
        public string Body;
        public DateTime Timestamp;
        
        public string GetTitleKey() {
            return Title;
        } 

        public string GetBodyKey() {
            return Body;
        }

        public DateTime GetTimestamp() {
            return Timestamp;
        }
    }
}
