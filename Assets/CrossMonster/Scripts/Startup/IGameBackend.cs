using MyLibrary;
using System;

namespace CrossMonsters {
    public interface ICrossBackend : IBasicBackend {
        DateTime GetDateTime();
    }
}
