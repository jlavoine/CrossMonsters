using System;

namespace MyLibrary {
    public interface IBasicNewsData {
        string GetTitleKey();
        string GetBodyKey();

        DateTime GetTimestamp();
    }
}
